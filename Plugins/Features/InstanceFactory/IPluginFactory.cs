using Plugins.Abstractions;

namespace Plugins.Features.InstanceFactory;

/// <summary>
/// Defines a contract for creating plugin instances dynamically.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Responsible for instantiating plugin objects given their <see cref="Type"/>.</item>
/// <item>Supports type-safe creation constrained to <see cref="IPluginBase"/>.</item>
/// <item>Returns <c>null</c> if instantiation fails or type is incompatible.</item>
/// </list>
/// </remarks>
public interface IPluginFactory
{
    /// <summary>
    /// Creates an instance of the specified plugin type.
    /// </summary>
    /// <typeparam name="TPlugin">The plugin interface or base class.</typeparam>
    /// <param name="type">The concrete <see cref="Type"/> to instantiate.</param>
    /// <returns>
    /// An instance of <typeparamref name="TPlugin"/> if successful; otherwise, <c>null</c>.
    /// </returns>
    TPlugin? CreateInstance<TPlugin>(Type type) where TPlugin : class, IPluginBase;
}