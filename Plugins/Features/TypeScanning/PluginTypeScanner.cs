using System.Reflection;
using Microsoft.Extensions.Logging;
using Plugins.Abstractions;
using Plugins.Abstractions.Attributes;
using Plugins.Abstractions.Enum;

namespace Plugins.Features.TypeScanning;

/// <summary>
/// Provides a concrete implementation of <see cref="IPluginTypeScanner"/> for discovering plugin types in assemblies.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Scans assemblies to find types implementing a specified plugin contract.</item>
/// <item>Supports filtering by <see cref="PluginType"/> and custom CLR type predicates.</item>
/// <item>Logs warnings when assembly types cannot be fully loaded.</item>
/// </list>
/// </remarks>
public sealed partial class PluginTypeScanner(ILogger<PluginTypeScanner> logger) : IPluginTypeScanner
{
    /// <inheritdoc />
    public IEnumerable<Type> GetPluginTypes<TPlugin>(
        Assembly assembly,
        PluginType? pluginType = null,
        Func<Type, bool>? typeFilter = null)
        where TPlugin : class, IPluginBase
    {
        Type[] types;

        try
        {
            types = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            LogFailedToLoadTypes(logger, ex, assembly.FullName ?? "Unknown");
            types = ex.Types.Where(t => t != null).ToArray()!;
        }

        foreach (var type in types)
        {
            if (type.IsAbstract || type.IsInterface)
                continue;

            if (!typeof(TPlugin).IsAssignableFrom(type))
                continue;

            if (typeFilter != null && !typeFilter(type))
                continue;

            if (pluginType != null)
            {
                var attr = type.GetCustomAttribute<PluginTypeAttribute>();
                if (attr == null || attr.Type != pluginType.Value)
                    continue;
            }

            yield return type;
        }
    }

    [LoggerMessage(LogLevel.Warning, "Failed to load types from assembly {assembly}", EventId = 0)]
    static partial void LogFailedToLoadTypes(ILogger<PluginTypeScanner> logger, ReflectionTypeLoadException ex, string assembly);
}