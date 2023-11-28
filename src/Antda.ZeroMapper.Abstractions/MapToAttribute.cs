namespace Antda.ZeroMapper.Abstractions;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class MapToAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}