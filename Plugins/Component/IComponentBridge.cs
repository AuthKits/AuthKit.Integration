namespace Plugins.Component;

/// <summary>
/// Represents a non-generic bridge to a component plugin, 
/// exposing its basic information and creation capability.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Provides access to the plugin's <see cref="Name"/>.</item>
/// <item>Exposes plugin <see cref="Metadata"/> including version and author.</item>
/// <item>Allows creation of the underlying component instance via <see cref="Create"/>.</item>
/// <item>Enables interaction with plugins in a non-generic context.</item>
/// </list>
/// </remarks>
public interface IComponentBridge
{
    /// <summary>
    /// Gets the name of the component plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets metadata associated with the plugin, such as version and author.
    /// </summary>
    IComponentPluginMetadata Metadata { get; }

    /// <summary>
    /// Creates an instance of the component managed by the plugin.
    /// </summary>
    /// <param name="services">
    /// Optional <see cref="IServiceProvider"/> for dependency injection during creation.
    /// </param>
    /// <returns>The created component instance as <see cref="object"/>.</returns>
    object Create(IServiceProvider? services = null);
}