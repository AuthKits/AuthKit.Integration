namespace Plugins.Abstractions;

/// <summary>
/// Defines the base contract for all plugins.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Provides plugin identity and lifecycle control.</item>
/// <item>Exposes standardized status and execution events.</item>
/// <item>Defines asynchronous initialization with runtime context.</item>
/// </list>
/// </remarks>
public interface IPluginBase
{
    /// <summary>
    /// Gets the unique name of the plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the plugin is enabled.
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// Occurs when the plugin finishes its execution cycle.
    /// </summary>
    event EventHandler? ExecutionCompleted;

    /// <summary>
    /// Occurs when the plugin reports a status message.
    /// </summary>
    event EventHandler<string>? StatusReported;

    /// <summary>
    /// Initializes the plugin with the provided runtime context.
    /// </summary>
    /// <param name="context">The runtime environment or host-specific context.</param>
    /// <returns>A task representing the asynchronous initialization operation.</returns>
    Task InitializeAsync(object context);
}