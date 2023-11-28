using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis.Models;

public class MethodParameter
{
  public MethodParameter(MethodParameterType type, IParameterSymbol symbol)
  {
    Type = type;
    Symbol = symbol;
  }

  public MethodParameterType Type { get; }

  public IParameterSymbol Symbol { get; }
}
