using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.Extensions;

public static class IncrementalValuesProviderExtensions
{
  public static IncrementalValuesProvider<T> WithoutNulls<T>(this IncrementalValuesProvider<T?> source)
    where T : notnull 
    => source.Where(item => item != null)!;

  public static IncrementalValuesProvider<T> WithoutNulls<T>(this IncrementalValuesProvider<T?> source)
    where T : struct
    => source.Where(item => item != null).Select((m, _) => m!.Value);
}