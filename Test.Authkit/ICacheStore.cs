namespace Test.Authkit;

/// <summary>
/// Defines the contract for a cache store implementation.
/// Components/plugins implement this interface to provide caching functionality.
/// </summary>
public interface ICacheStore
{
    /// <summary>
    /// Sets a value in the cache with a given key.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <param name="key">Cache key.</param>
    /// <param name="value">Value to store.</param>
    void Set<T>(string key, T value);

    /// <summary>
    /// Attempts to get a value from the cache by key.
    /// </summary>
    /// <typeparam name="T">Expected type of the value.</typeparam>
    /// <param name="key">Cache key.</param>
    /// <param name="value">Output value if found.</param>
    /// <returns>True if value exists, false otherwise.</returns>
    bool TryGet<T>(string key, out T value);

    /// <summary>
    /// Removes a value from the cache.
    /// </summary>
    /// <param name="key">Cache key.</param>
    void Remove(string key);
}