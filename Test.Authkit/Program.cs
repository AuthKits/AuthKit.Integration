using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plugins;
using Plugins.Features.AssemblyLoading;
using Test.Authkit;

var serviceCollection = new ServiceCollection();

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<AssemblyLoader>();
var loader = new ComponentLoader(new AssemblyLoader(logger));

var cacheComponents = loader.LoadComponents<ICacheStore>("components");

foreach (var component in cacheComponents)
{
    var impl = component.Create(null);
    serviceCollection.AddSingleton(impl);
    Console.WriteLine($"\nLoaded component: {component.Name} Version: {component.Version}");
    Console.WriteLine($"Author: {component.Author}");
    Console.WriteLine($"Description: {component.Description}");
}
var services = serviceCollection.BuildServiceProvider();
var cache = services.GetService<ICacheStore>();

cache!.Set("test", 123);
if (cache.TryGet<int>("test", out var value))
{
    Console.WriteLine($"Cache test key='test' value={value}");
}
