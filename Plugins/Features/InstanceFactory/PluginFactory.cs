using Microsoft.Extensions.Logging;
using Plugins.Abstractions;

namespace Plugins.Features.InstanceFactory;

/// <summary>
/// Provides a concrete implementation of <see cref="IPluginFactory"/> for creating plugin instances dynamically.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Responsible for instantiating plugin objects given their <see cref="Type"/>.</item>
/// <item>Logs creation success and failure using <see cref="ILogger{TCategoryName}"/>.</item>
/// <item>Returns <c>null</c> if instantiation fails or the type is incompatible with <see cref="IPluginBase"/>.</item>
/// </list>
/// </remarks>
public sealed partial class PluginFactory(ILogger<PluginFactory> logger) : IPluginFactory
{
    /// <inheritdoc />
    public TPlugin? CreateInstance<TPlugin>(Type? type) where TPlugin : class, IPluginBase
    {
        if (type == null) return null;

        TPlugin? instance;

        try
        {
            instance = Activator.CreateInstance(type) as TPlugin;
            if (instance != null)
                LogCreatedInstanceOfPluginTypeTypename(logger, type.FullName);
        }
        catch (Exception ex)
        {
            LogFailedToCreatePluginInstance(logger, type.FullName ?? "Unknown", ex);
            instance = null;
        }

        return instance;
    }
    
    [LoggerMessage(LogLevel.Warning, "Failed to create plugin instance for type {typeName}", EventId = 0)]
    static partial void LogFailedToCreatePluginInstance(ILogger<PluginFactory> logger, string typeName, Exception ex);

    [LoggerMessage(LogLevel.Debug, "Created instance of plugin type: {typeName}")]
    static partial void LogCreatedInstanceOfPluginTypeTypename(ILogger<PluginFactory> logger, string? typeName);
}
