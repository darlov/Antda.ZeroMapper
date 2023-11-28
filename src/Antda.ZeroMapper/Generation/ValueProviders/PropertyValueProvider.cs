using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.Generation.ValueProviders;

public class PropertyValueProvider : IValueProvider
{
  public PropertyValueProvider(IPropertySymbol symbol)
  {
    Symbol = symbol;
  }

  public IPropertySymbol Symbol { get; }

  public string MapToName => Symbol.Name;
}