using Microsoft.Extensions.Logging;
using Plugins.Abstractions;

namespace Plugins.Features.Initialization;

/// <summary>
/// Provides a concrete implementation of <see cref="IPluginInitializer"/> for initializing plugin instances.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Handles asynchronous initialization of <see cref="IPluginBase"/> plugins.</item>
/// <item>Logs success and failure using <see cref="ILogger{TCategoryName}"/>.</item>
/// <item>Ensures that exceptions during initialization are caught and logged without throwing.</item>
/// </list>
/// </remarks>
public sealed partial class PluginInitializer(ILogger<PluginInitializer> logger) : IPluginInitializer
{
    /// <inheritdoc />
    public async Task<bool> InitializeAsync(IPluginBase? plugin, object? context = null)
    {
        if (plugin == null) return false;

        try
        {
            await plugin.InitializeAsync(context ?? new object());
        }
        catch (Exception ex)
        {
            LogFailedToInitializePlugin(logger, plugin.Name, ex);
            return false;
        }

        LogInitializedPluginPluginname(logger, plugin.Name);
        return true;
    }

    [LoggerMessage(LogLevel.Warning, "Failed to initialize plugin {pluginName}", EventId = 0)]
    static partial void LogFailedToInitializePlugin(ILogger<PluginInitializer> logger, string pluginName, Exception ex);

    [LoggerMessage(LogLevel.Information, "Initialized plugin: {PluginName}")]
    static partial void LogInitializedPluginPluginname(ILogger<PluginInitializer> logger, string PluginName);
}