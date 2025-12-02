namespace Plugins.Component;

/// <summary>
/// Represents a component plugin that can create instances of a specific type <typeparamref name="T"/> 
/// and exposes metadata about the plugin.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Provides a unique <see cref="Name"/> for identification.</item>
/// <item>Exposes plugin metadata such as version and author through <see cref="IComponentMetadata"/>.</item>
/// <item>Supports creating instances of the component via <see cref="Create"/>.</item>
/// <item>Allows optional initialization logic through <see cref="Initialize"/>.</item>
/// <item>Can be used in a generic or non-generic context via bridges like <see cref="IComponentBridge"/>.</item>
/// </list>
/// </remarks>
/// <typeparam name="T">The type of component created by the plugin.</typeparam>
public interface IComponent<out T> : IComponentMetadata
{
    /// <summary>
    /// Gets the unique name of the plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Creates an instance of the component.
    /// </summary>
    /// <param name="services">
    /// Optional <see cref="IServiceProvider"/> for dependency injection during creation.
    /// </param>
    /// <returns>The created component instance of type <typeparamref name="T"/>.</returns>
    T Create(IServiceProvider? services = null);

    /// <summary>
    /// Performs optional initialization logic when the plugin is loaded.
    /// </summary>
    /// <param name="services">
    /// Optional <see cref="IServiceProvider"/> for dependency injection during initialization.
    /// </param>
    void Initialize(IServiceProvider? services = null);
}