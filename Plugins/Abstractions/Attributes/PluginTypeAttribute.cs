using Plugins.Abstractions.Enum;

namespace Plugins.Abstractions.Attributes;

/// <summary>
/// Specifies the logical type of a plugin.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Used for plugin classification and discovery.</item>
/// <item>Applied at class level.</item>
/// <item>Enables filtering and conditional loading of plugins.</item>
/// </list>
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class PluginTypeAttribute(PluginType type) : Attribute
{
    /// <summary>
    /// Gets the assigned plugin type.
    /// </summary>
    public PluginType Type { get; } = type;
}