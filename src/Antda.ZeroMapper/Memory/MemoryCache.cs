using System.Collections.Concurrent;

namespace Antda.ZeroMapper.Memory;

public class MemoryCache : IMemoryCache
{
  private readonly ConcurrentDictionary<object, object> _values = new();
  
  public TVal GetOrAdd<TKey, TVal>(TKey key, Func<TKey, TVal> factory)
    where TKey : notnull
    where TVal : notnull =>
    (TVal)_values.GetOrAdd(key, (m) => factory((TKey)m));
}