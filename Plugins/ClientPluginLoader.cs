using Plugins.Abstractions;
using Plugins.Abstractions.Enum;
using Plugins.Features.AssemblyLoading;
using Plugins.Features.Initialization;
using Plugins.Features.InstanceFactory;
using Plugins.Features.TypeScanning;

namespace Plugins;

public class PluginLoader(
    IAssemblyLoader assemblyLoader,
    IPluginTypeScanner scanner,
    IPluginFactory factory,
    IPluginInitializer initializer)
    : IPluginLoader
{
    public async Task<IEnumerable<TPlugin>> LoadPluginsAsync<TPlugin>(
        string path,
        PluginType? pluginType = null,
        string? nameFilter = null,
        Func<Type, bool>? typeFilter = null,
        object? context = null
    )
        where TPlugin : class, IPluginBase
    {
        var result = new List<TPlugin>();

        foreach (var assembly in assemblyLoader.LoadAssemblies(path))
        {
            foreach (var type in scanner.GetPluginTypes<TPlugin>(
                         assembly, pluginType, typeFilter))
            {
                var instance = factory.CreateInstance<TPlugin>(type);
                if (instance == null)
                    continue;

                if (nameFilter != null && instance.Name != nameFilter)
                    continue;

                if (await initializer.InitializeAsync(instance, context))
                    result.Add(instance);
            }
        }

        return result;
    }
}