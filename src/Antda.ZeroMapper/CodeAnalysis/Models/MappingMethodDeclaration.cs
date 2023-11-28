using Antda.ZeroMapper.Generation;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis.Models;

public class MappingMethodDeclaration
{
  public MappingMethodDeclaration(
    IMethodSymbol symbol,
    IParameterSymbol mapFromParameter,
    IReadOnlyCollection<IValueProvider> valueProviders,
    IReadOnlyCollection<IValueSetter> valueSetters)
  {
    Symbol = symbol;
    MapFromParameter = mapFromParameter;
    ValueProviders = valueProviders;
    ValueSetters = valueSetters;
  }

  public IMethodSymbol Symbol { get;}

  public IParameterSymbol MapFromParameter { get; }
  
  public IReadOnlyCollection<IValueProvider> ValueProviders { get; }
  public IReadOnlyCollection<IValueSetter> ValueSetters { get; }
}