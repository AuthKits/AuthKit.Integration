using Plugins.Abstractions;
using Plugins.Component;
using Plugins.Features.AssemblyLoading;

namespace Plugins;

/// <inheritdoc />
public class ComponentLoader(IAssemblyLoader assemblyLoader) : IComponentLoader
{
    /// <inheritdoc />
    public IEnumerable<IComponentPlugin<T>> LoadComponents<T>(string path)
    {
        var plugins = new List<IComponentPlugin<T>>();

        foreach (var asm in assemblyLoader.LoadAssemblies(path))
        {
            foreach (var type in asm.GetTypes())
            {
                if (typeof(IComponentPlugin<T>).IsAssignableFrom(type) &&
                    type is { IsAbstract: false, IsInterface: false })
                {
                    var instance = (IComponentPlugin<T>)Activator.CreateInstance(type)!;
                    plugins.Add(instance);
                }
            }
        }

        return plugins;
    }
}