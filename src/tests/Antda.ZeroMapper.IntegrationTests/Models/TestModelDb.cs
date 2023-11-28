namespace Antda.ZeroMapper.IntegrationTests.Models;

public class TestModelDb
{
    public int Id { get; set; }

    public int CtorValue { get; set; }

    public int IntValue { get; set; }

    public int IntInitOnlyValue { get; init; }
    
    public required int RequiredValue { get; init; }

    public string StringValue { get; set; } = string.Empty;

    public string RenamedStringValue { get; set; } = string.Empty;

    public int UnflatteningIdValue { get; set; }

    public int? NullableUnflatteningIdValue { get; set; }

    public string? StringNullableTargetNotNullable { get; set; }

    public TestModelDb? RecursiveObject { get; set; }

    public TestModelDb? SourceTargetSameObjectType { get; set; }

    public string EnumReverseStringValue { get; set; } = string.Empty;

    public string? IgnoredStringValue { get; set; }

    public int IgnoredIntValue { get; set; }

    public DateTime DateTimeValueTargetDateOnly { get; set; }

    public DateTime DateTimeValueTargetTimeOnly { get; set; }

    public string ManuallyMapped { get; set; } = "fooBar";
}