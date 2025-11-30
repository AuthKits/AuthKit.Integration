using System.Reflection;
using Plugins.Abstractions;
using Plugins.Abstractions.Enum;

namespace Plugins.Features.TypeScanning;

/// <summary>
/// Defines a contract for scanning assemblies and retrieving plugin types.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Responsible for discovering types that implement a given plugin contract.</item>
/// <item>Supports optional filtering by logical <see cref="PluginType"/>.</item>
/// <item>Supports additional CLR type predicate filtering.</item>
/// </list>
/// </remarks>
public interface IPluginTypeScanner
{
    /// <summary>
    /// Retrieves all plugin types from the specified assembly that match the given contract and filters.
    /// </summary>
    /// <typeparam name="TPlugin">The plugin interface or base class to search for.</typeparam>
    /// <param name="assembly">The assembly to scan for plugin types.</param>
    /// <param name="pluginType">Optional logical plugin type filter.</param>
    /// <param name="typeFilter">Optional CLR type predicate filter.</param>
    /// <returns>An enumerable of <see cref="Type"/> objects representing matching plugin types.</returns>
    IEnumerable<Type> GetPluginTypes<TPlugin>(
        Assembly assembly,
        PluginType? pluginType = null,
        Func<Type, bool>? typeFilter = null
    ) where TPlugin : class, IPluginBase;
}