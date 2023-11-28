using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.Generation.ValueSetters;

public class PropertyValueSetter : IValueSetter
{
  public PropertyValueSetter(IPropertySymbol symbol)
  {
    Symbol = symbol;
  }

  public IPropertySymbol Symbol { get; }

  public string MapToName => Symbol.Name;
}