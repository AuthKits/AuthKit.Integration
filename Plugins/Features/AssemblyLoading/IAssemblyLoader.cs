using System.Reflection;

namespace Plugins.Features.AssemblyLoading;

/// <summary>
/// Defines a contract for loading .NET assemblies from a specified location.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Responsible for discovering and loading assemblies from file system paths.</item>
/// <item>Can be used as a building block for plugin loading systems.</item>
/// <item>Does not instantiate types—only loads <see cref="Assembly"/> instances.</item>
/// </list>
/// </remarks>
public interface IAssemblyLoader
{
    /// <summary>
    /// Loads all assemblies found in the specified directory path.
    /// </summary>
    /// <param name="path">The directory path to search for assemblies.</param>
    /// <param name="dllFilter">Optional filter for DLL file names.</param>
    /// <returns>An enumerable of loaded <see cref="Assembly"/> instances.</returns>
    IEnumerable<Assembly> LoadAssemblies(string path, Func<string, bool>? dllFilter = null);

    /// <summary>
    /// Loads a single assembly from the specified DLL file path.
    /// </summary>
    /// <param name="dllPath">The file path of the DLL to load.</param>
    /// <returns>The loaded <see cref="Assembly"/> or null if loading failed.</returns>
    Assembly? LoadAssembly(string dllPath);
}