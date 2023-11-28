using Antda.ZeroMapper.CodeAnalysis.AttributesData;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis.Models;

public class MapToMethodParameter : MethodParameter
{
  public MapToMethodParameter(MethodParameterType type, IParameterSymbol symbol, MapToAttributeData data) : base(type, symbol)
  {
    Data = data;
  }

  public MapToAttributeData Data { get; }
}