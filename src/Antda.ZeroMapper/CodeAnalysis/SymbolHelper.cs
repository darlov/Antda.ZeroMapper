using System.Linq.Expressions;
using System.Reflection;
using Antda.ZeroMapper.CodeAnalysis.AttributesData;
using Antda.ZeroMapper.Extensions;
using Antda.ZeroMapper.Memory;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis;

public class SymbolHelper : ISymbolHelper
{
  private readonly ICompilationContext _context;
  private readonly IMemoryCache _memoryCache;

  public SymbolHelper(ICompilationContext context, IMemoryCache memoryCache)
  {
    _context = context;
    _memoryCache = memoryCache;
  }

  public IReadOnlyCollection<TData> GetAttributes<T, TData>(ISymbol symbol)
    where T : Attribute
    where TData : BaseAttributeData
  {
    var key = new AttributeDataCacheKey(symbol, typeof(TData));

    return _memoryCache.GetOrAdd(key, (k) =>
      {
        var attributes = k.Symbol.GetAttributes();

        if (attributes.IsEmpty)
        {
          return (IReadOnlyCollection<TData>)Array.Empty<TData>();
        }



        var attributeSymbol = _context.GetTypeByMetadataName(typeof(T).FullName!);
        var constructor = k.DataType.GetConstructors(BindingFlags.Instance | BindingFlags.Public).First();
        var dataParams = constructor.GetParameters();
        var dataProps = k.DataType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        var attrParameter = Expression.Parameter(typeof(AttributeData));

        var constructorExpressionParams = new List<Expression>
        {
          attrParameter
        };
        var thisVar = Expression.Constant(this);

        Expression<Func<AttributeData, object?>> test = val => GetValue<string>(val.ConstructorArguments[0]);

        for (int i = 1; i < dataParams.Length; i++)
        {
          var parameterInfo = dataParams[i];

          var getValueMethod = this.GetType()
            .GetMethod(nameof(GetValue), BindingFlags.NonPublic | BindingFlags.Instance, null , new[] { typeof(TypedConstant) }, null)!
            .MakeGenericMethod(parameterInfo.ParameterType);

          var indexAccess =Expression.Property(Expression.Property(attrParameter, "ConstructorArguments"), "Item", Expression.Constant(i - 1));
          var call = Expression.Call(thisVar, getValueMethod, indexAccess);
          constructorExpressionParams.Add(call);
        }

        var tt = Expression.MemberInit(Expression.New(constructor, constructorExpressionParams));

        var list = new List<TData>();
        foreach (var attr in attributes.Where(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass?.ConstructedFrom ?? attr.AttributeClass, attributeSymbol)))
        {
          IEnumerable<object> constructorParams = new[] { attr };
          constructorParams = constructorParams.Concat(attr.ConstructorArguments.Select((m, i) => GetValue(m, dataParams[i].ParameterType)).WithoutNulls());
          var instance = (TData)Activator.CreateInstance(typeof(TData), constructorParams.ToArray());

          foreach (var namedArgument in attr.NamedArguments)
          {
            var prop = dataProps.First(m => m.Name == namedArgument.Key);
            prop.SetValue(instance, GetValue(namedArgument.Value, prop.PropertyType));
          }

          list.Add(instance);
        }

        return list;
      }
    );
  }

  private T? GetValue<T>(TypedConstant arg)
  {
    return arg.Kind switch
    {
      _ when arg.IsNull => default,
      TypedConstantKind.Enum => GetEnumValue<T>(arg),
      //TypedConstantKind.Array => BuildArrayValue(arg, targetType),
      TypedConstantKind.Primitive => (T?)arg.Value,
      TypedConstantKind.Type when typeof(T) == typeof(ITypeSymbol) => (T?)arg.Value,
      _ => throw new ArgumentOutOfRangeException($"{arg.Kind.ToString()} does not support or cannot converted to {typeof(T)}"),
    };
  }

  private T? GetEnumValue<T>(TypedConstant arg)
  {
    if (arg.Value == null)
    {
      return default;
    }

    var type = typeof(T);

    if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
    {
      type = Nullable.GetUnderlyingType(type)!;
    }

    return (T?)Enum.ToObject(type, arg.Value!);
  }

  private object? GetValue(TypedConstant arg, Type targetType)
  {
    return arg.Kind switch
    {
      _ when arg.IsNull => null,
      TypedConstantKind.Enum => GetEnumValue(arg, targetType),
      //TypedConstantKind.Array => BuildArrayValue(arg, targetType),
      TypedConstantKind.Primitive => arg.Value,
      TypedConstantKind.Type when targetType == typeof(ITypeSymbol) => arg.Value,
      _ => throw new ArgumentOutOfRangeException($"{arg.Kind.ToString()} does not support or cannot converted to {targetType}"),
    };
  }

  private object? GetEnumValue(TypedConstant arg, Type targetType)
  {
    if (arg.Value == null)
    {
      return null;
    }

    if (targetType.IsConstructedGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
    {
      targetType = Nullable.GetUnderlyingType(targetType)!;
    }

    return Enum.ToObject(targetType, arg.Value!);
  }

  private record AttributeDataCacheKey(ISymbol Symbol, Type DataType);
}