namespace Antda.ZeroMapper.Extensions;

public static class EnumerableExtensions
{
  public static IEnumerable<T> WithoutNulls<T>(this IEnumerable<T?> source)
    => source.Where(item => item != null)!;

  public static IEnumerable<T> WithoutNulls<T>(this IEnumerable<T?> source) 
    where T : struct
    => source.Where(item => item != null).Select(m => m!.Value);
}