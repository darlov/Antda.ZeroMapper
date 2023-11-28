using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis.AttributesData;

public record MapToAttributeData(AttributeData Data, string Name) : BaseAttributeData(Data);