using Plugins.Component;
using Test.Authkit;

namespace AuthKit.Component.Storage.InMemory;

public class InMemoryCacheComponent : IComponent<ICacheStore>
{
    public string Name => "InMemory Cache Component";
    public string Version => "1.0.0";
    public string Author => "Zarin";
    public string Description => "In-memory cache implementation for AuthKit.";

    public void Initialize(IServiceProvider? services = null)
    {
        Console.WriteLine($"{Name} initialized.");
    }

    public ICacheStore Create(IServiceProvider? services = null)
    {
        return new InMemoryCacheStore();
    }
}