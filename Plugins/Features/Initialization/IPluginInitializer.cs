using Plugins.Abstractions;

namespace Plugins.Features.Initialization;

/// <summary>
/// Defines a contract for initializing plugin instances with an optional runtime context.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Responsible for executing plugin-specific initialization logic.</item>
/// <item>Supports passing an optional context to the plugin.</item>
/// <item>Returns a success flag indicating whether initialization succeeded.</item>
/// </list>
/// </remarks>
public interface IPluginInitializer
{
    /// <summary>
    /// Asynchronously initializes the given plugin instance.
    /// </summary>
    /// <param name="plugin">The plugin instance to initialize.</param>
    /// <param name="context">Optional runtime context for initialization.</param>
    /// <returns>
    /// A task representing the asynchronous operation, 
    /// containing <c>true</c> if initialization succeeded, otherwise <c>false</c>.
    /// </returns>
    Task<bool> InitializeAsync(IPluginBase plugin, object? context = null);
}