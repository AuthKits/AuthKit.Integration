using Plugins.Component;

namespace Plugins.Abstractions;

/// <summary>
/// Defines a loader responsible for discovering and loading component plugins from a given location.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Supports loading plugins of a specific component type <typeparamref name="T"/>.</item>
/// <item>Returns strongly typed instances of <see cref="IComponentPlugin{T}"/>.</item>
/// <item>Enables dynamic discovery of plugins from external paths or directories.</item>
/// </list>
/// </remarks>
public interface IComponentLoader
{
    /// <summary>
    /// Loads all component plugins of type <typeparamref name="T"/> from the specified path.
    /// </summary>
    /// <param name="path">The filesystem path or directory where plugins are located.</param>
    /// <returns>
    /// A collection of loaded plugins implementing <see cref="IComponentPlugin{T}"/>.
    /// </returns>
    IEnumerable<IComponentPlugin<T>> LoadComponents<T>(string path);
}