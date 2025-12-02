using Test.Authkit;

namespace AuthKit.Component.Storage.InMemory;

public class InMemoryCacheStore : ICacheStore
{
    private readonly Dictionary<string, object> _cache = new();

    public void Set<T>(string key, T value)
    {
        _cache[key] = value!;
    }

    public bool TryGet<T>(string key, out T value)
    {
        if (_cache.TryGetValue(key, out var obj) && obj is T t)
        {
            value = t;
            return true;
        }

        value = default!;
        return false;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}