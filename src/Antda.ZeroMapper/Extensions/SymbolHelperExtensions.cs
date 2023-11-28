using Antda.ZeroMapper.CodeAnalysis;
using Antda.ZeroMapper.CodeAnalysis.AttributesData;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.Extensions;

public static class SymbolHelperExtensions
{
  public static TData? GetAttribute<T, TData>(this ISymbolHelper symbolHelper, ISymbol symbol)
    where T : Attribute
    where TData : BaseAttributeData
    => symbolHelper.GetAttributes<T, TData>(symbol).FirstOrDefault();

  public static TData GetRequiredAttribute<T, TData>(this ISymbolHelper symbolHelper, ISymbol symbol)
    where T : Attribute
    where TData : BaseAttributeData
    => symbolHelper.GetAttributes<T, TData>(symbol).First();
}