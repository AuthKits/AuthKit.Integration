namespace Plugins.Component;

/// <summary>
/// Represents metadata associated with a component plugin.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Provides optional version information for caching or updates.</item>
/// <item>Exposes the author or owner of the plugin.</item>
/// <item>Allows adding an optional description of the plugin's purpose.</item>
/// <item>Serves as a base for generic and non-generic plugin interfaces like <see cref="IComponentPlugin{T}"/>.</item>
/// </list>
/// </remarks>
public interface IComponentPluginMetadata
{
    /// <summary>
    /// Optional plugin version or identifier, useful for caching or updates.
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Optional plugin author or owner.
    /// </summary>
    string Author { get; }

    /// <summary>
    /// Optional description explaining the plugin's functionality.
    /// </summary>
    string Description { get; }
}