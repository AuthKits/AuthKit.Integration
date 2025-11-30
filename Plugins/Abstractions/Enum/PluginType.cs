namespace Plugins.Abstractions.Enum;

/// <summary>
/// Defines the execution context of a plugin.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item><see cref="Client"/> – plugin intended to run on the client side.</item>
/// <item><see cref="Server"/> – plugin intended to run on the server side.</item>
/// </list>
/// </remarks>
public enum PluginType
{
    Client,
    Server
}
