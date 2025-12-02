using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Plugins.Features.AssemblyLoading;

/// <summary>
/// Provides a concrete implementation of <see cref="IAssemblyLoader"/> that loads assemblies from the file system.
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>Discovers and loads all DLL assemblies from a specified directory.</item>
/// <item>Logs warnings if the directory does not exist or if a specific assembly fails to load.</item>
/// <item>Returns successfully loaded <see cref="Assembly"/> instances for further plugin scanning.</item>
/// </list>
/// </remarks>
public sealed partial class AssemblyLoader(ILogger<AssemblyLoader> logger) : IAssemblyLoader
{
    private readonly HashSet<string> _loadedDlls = [];

    /// <inheritdoc />
    public IEnumerable<Assembly> LoadAssemblies(string path, Func<string, bool>? dllFilter = null)
    {
        if (!Directory.Exists(path))
        {
            LogPluginDirectoryNotFoundPath(logger, path);
            yield break;
        }

        foreach (var dll in Directory.GetFiles(path, "*.dll"))
        {
            if (dllFilter != null && !dllFilter(dll))
                continue;
            
            if (!_loadedDlls.Add(dll))
                continue;

            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(dll);
            }
            catch (Exception ex)
            {
                LogFailedToLoadAssemblyDll(logger, dll, ex);
                continue;
            }

            LogSuccessfullyLoadedAssemblyDll(logger, dll);
            yield return assembly;
        }
    }

    /// <inheritdoc />
    public bool HasAssemblies(string path)
    {
        if (!Directory.Exists(path))
        {
            LogPluginDirectoryNotFoundPath(logger, path);
            return false;
        }

        return Directory.GetFiles(path, "*.dll").Any();
    }

    /// <inheritdoc />
    public Assembly? LoadAssembly(string dllPath)
    {
        if (!File.Exists(dllPath))
        {
            LogDllNotFound(logger, dllPath);
            return null;
        }

        try
        {
            return Assembly.LoadFrom(dllPath);
        }
        catch (Exception ex)
        {
            LogFailedToLoadAssemblyDll(logger, dllPath, ex);
            return null;
        }
    }
}
