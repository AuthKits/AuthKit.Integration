using Plugins.Abstractions;
using Plugins.Component;
using Plugins.Features.AssemblyLoading;

namespace Plugins;

/// <inheritdoc />
public class ComponentLoader(IAssemblyLoader assemblyLoader) : IComponentLoader
{
    /// <inheritdoc />
    public IEnumerable<IComponent<T>> LoadComponents<T>(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Component directory not found: {path}");

        var components = new List<IComponent<T>>();

        foreach (var asm in assemblyLoader.LoadAssemblies(path))
        {
            foreach (var type in asm.GetTypes())
            {
                if (!typeof(IComponent<T>).IsAssignableFrom(type) ||
                    type.IsAbstract || type.IsInterface) 
                    continue;

                IComponent<T>? instance;

                try
                {
                    instance = (IComponent<T>?)Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"Failed to create instance of component {type.FullName} in assembly {asm.FullName}", ex);
                }

                if (instance == null)
                    throw new InvalidOperationException(
                        $"Component {type.FullName} in assembly {asm.FullName} could not be instantiated.");

                components.Add(instance);
            }
        }

        return components;
    }
}