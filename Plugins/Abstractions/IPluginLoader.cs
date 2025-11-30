using Plugins.Abstractions.Enum;

namespace Plugins.Abstractions;

/// <summary>
/// Defines a contract for dynamically loading plugins from external sources.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Supports generic, type-safe plugin loading.</item>
/// <item>Allows filtering by plugin type, name, and CLR type.</item>
/// <item>Handles optional runtime initialization context.</item>
/// </list>
/// </remarks>
public interface IPluginLoader
{
    Task<IEnumerable<TPlugin>> LoadPluginsAsync<TPlugin>(
        string path,
        PluginType? pluginType = null,
        string? nameFilter = null,
        Func<Type, bool>? typeFilter = null,
        object? context = null
    ) where TPlugin : class, IPluginBase;
}