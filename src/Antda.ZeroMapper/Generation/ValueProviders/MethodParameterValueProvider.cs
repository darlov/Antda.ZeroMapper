using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.Generation.ValueProviders;

public class MethodParameterValueProvider(IParameterSymbol symbol, string mapToName) : IValueProvider
{
  public IParameterSymbol Symbol { get; } = symbol;

  public string MapToName { get; } = mapToName;
}