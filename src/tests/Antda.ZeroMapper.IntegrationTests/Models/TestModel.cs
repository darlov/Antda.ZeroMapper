namespace Antda.ZeroMapper.IntegrationTests.Models;

public class TestModel
{
  public int CtorValue { get; set; }

  public int CtorValue2 { get; set; }

  public int IntValue { get; set; }

  public int IntInitOnlyValue { get; init; }
  
  public required int RequiredValue { get; init; }

  public int UnmappedValue => 10;

  public string StringValue { get; set; } = string.Empty;

  public string RenamedStringValue { get; set; } = string.Empty;

  public int UnflatteningIdValue { get; set; }

  public int? NullableUnflatteningIdValue { get; set; }
  

  public string? StringNullableTargetNotNullable { get; set; }

  public (string A, string)? TupleValue { get; set; }

  public TestModel? RecursiveObject { get; set; }

  public TestModel? SourceTargetSameObjectType { get; set; }

  public Span<string> SpanValue => new[] { "1", "2", "3" };

  public Memory<string> MemoryValue { get; set; }

  public Stack<string> StackValue { get; set; } = new();

  public Queue<string> QueueValue { get; set; } = new();
  
  public ISet<string> ExistingISet { get; } = new HashSet<string>();

  public HashSet<string> ExistingHashSet { get; } = new();

  public SortedSet<string> ExistingSortedSet { get; } = new();

  public List<string> ExistingList { get; } = new();

  public ISet<string> ISet { get; set; } = new HashSet<string>();

  public IReadOnlySet<string> IReadOnlySet { get; set; } = new HashSet<string>();

  public HashSet<string> HashSet { get; set; } = new HashSet<string>();

  public SortedSet<string> SortedSet { get; set; } = new SortedSet<string>();


  public string EnumReverseStringValue { get; set; } = string.Empty;

  public string? IgnoredStringValue { get; set; }

  public int IgnoredIntValue { get; set; }

  public DateTime DateTimeValue { get; set; }

  public DateTime DateTimeValueTargetDateOnly { get; set; }

  public DateTime DateTimeValueTargetTimeOnly { get; set; }

  public int ExposePrivateValue => PrivateValue;

  private int PrivateValue { get; set; }
}