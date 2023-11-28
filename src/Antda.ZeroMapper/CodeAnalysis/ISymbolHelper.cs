using Antda.ZeroMapper.CodeAnalysis.AttributesData;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis;

public interface ISymbolHelper
{
  IReadOnlyCollection<TData> GetAttributes<T, TData>(ISymbol symbol)
    where T : Attribute
    where TData : BaseAttributeData;
}