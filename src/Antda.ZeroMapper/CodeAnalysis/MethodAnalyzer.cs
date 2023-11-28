using Antda.ZeroMapper.Abstractions;
using Antda.ZeroMapper.CodeAnalysis.AttributesData;
using Antda.ZeroMapper.CodeAnalysis.Models;
using Antda.ZeroMapper.Extensions;
using Antda.ZeroMapper.Generation;
using Antda.ZeroMapper.Generation.ValueProviders;
using Antda.ZeroMapper.Generation.ValueSetters;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis;

public class MethodAnalyzer : IMethodAnalyzer
{
  private readonly ICompilationContext _context;

  public MethodAnalyzer(ICompilationContext context)
  {
    _context = context;
  }

  public IEnumerable<MappingMethodDeclaration> GetMethods(MappingClassSymbol symbol, CancellationToken cancellationToken)
  {
    foreach (var methodSymbol in symbol.TargetSymbol.GetMembers().OfType<IMethodSymbol>())
    {
      cancellationToken.ThrowIfCancellationRequested();

      if (methodSymbol is { IsPartialDefinition: true })
      {

        if (methodSymbol.IsStatic)
        {
          // TODO: Error method can't be static
          yield break;
        }

       var methodParameters = GetParameters(methodSymbol.Parameters, cancellationToken).ToList();


        //var valueProviders = GetValueProviders(methodSymbol, cancellationToken).ToList();
        var valueSetters = GetValueSetters(methodSymbol, cancellationToken).ToList();

        yield return null; //new MappingMethodDeclaration(methodSymbol, mapFrom, valueProviders, valueSetters);
      }
    }
  }

  private IEnumerable<IValueSetter> GetValueSetters(IMethodSymbol methodSymbol, CancellationToken cancellationToken)
  {
    var returnType = methodSymbol.ReturnType;
    foreach (var propertySymbol in returnType.GetMembers().OfType<IPropertySymbol>().Where(m => !m.IsStatic))
    {
      cancellationToken.ThrowIfCancellationRequested();
      yield return new PropertyValueSetter(propertySymbol);
    }
  }

  // private IEnumerable<IValueProvider> GetValueProviders(IMethodSymbol methodSymbol, CancellationToken cancellationToken)
  // {
  //   var mapFrom = methodSymbol.Parameters.First();
  //   var parameters = GetParameters(methodSymbol.Parameters, cancellationToken).ToList();
  //
  //   foreach (var parametersOverride in parametersOverrides)
  //   {
  //     cancellationToken.ThrowIfCancellationRequested();
  //     yield return parametersOverride;
  //   }
  //
  //   foreach (var propertySymbol in mapFrom.Type.GetMembers().OfType<IPropertySymbol>().Where(m => !m.IsStatic))
  //   {
  //     cancellationToken.ThrowIfCancellationRequested();
  //     var propertyValueProvider = new PropertyValueProvider(propertySymbol);
  //
  //     if (parametersOverrides.All(m => m.MapToName != propertyValueProvider.MapToName))
  //     {
  //       yield return propertyValueProvider;
  //     }
  //   }
  // }

  private IEnumerable<MethodParameter> GetParameters(IReadOnlyList<IParameterSymbol> parameters, CancellationToken cancellationToken)
  {
    if (parameters.Count == 0)
    {
      // TODO: Error method should has a least one parameter
    }

    for (var i = 0; i < parameters.Count; i++)
    {
      var parameter = parameters[i];

      yield return GetParameterType(i, parameter) switch
      {
        MethodParameterType.MapFrom => new MethodParameter(MethodParameterType.MapFrom, parameter),
        MethodParameterType.MapToOverride => new MapToMethodParameter(MethodParameterType.MapFrom, parameter, _context.SymbolHelper.GetRequiredAttribute<MapToAttribute, MapToAttributeData>(parameter)),
        _ => throw new ArgumentOutOfRangeException()
      };
    }
  }

  private MethodParameterType GetParameterType(int index, IParameterSymbol parameterSymbol)
  {
    const int MapFromIndex = 0;

    if (MapFromIndex == index)
    {
      return MethodParameterType.MapFrom;
    }

    var mapToAttribute = _context.SymbolHelper.GetAttribute<MapToAttribute, MapToAttributeData>(parameterSymbol);
    if (mapToAttribute != null)
    {
      return MethodParameterType.MapToOverride;
    }

    return MethodParameterType.None;
  }
}