namespace Plugins.Component;

/// <inheritdoc />
public class ComponentBridge<T>(IComponent<T> plugin) : IComponentBridge
{
    /// <inheritdoc />
    public string Name => plugin.Name;
    
    /// <inheritdoc />
    public IComponentMetadata Metadata => plugin;
    
    /// <inheritdoc />
    public object Create(IServiceProvider? services = null) => plugin.Create(services) ?? throw new InvalidOperationException();
}