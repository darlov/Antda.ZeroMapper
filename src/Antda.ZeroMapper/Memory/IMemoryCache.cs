namespace Antda.ZeroMapper.Memory;

public interface IMemoryCache
{
  TVal GetOrAdd<TKey, TVal>(TKey key, Func<TKey, TVal> factory)
    where TKey : notnull
    where TVal : notnull;
}