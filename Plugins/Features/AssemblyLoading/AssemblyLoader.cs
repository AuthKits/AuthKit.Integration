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
    /// <inheritdoc />
    public IEnumerable<Assembly> LoadAssemblies(string path)
    {
        if (!Directory.Exists(path))
        {
            LogPluginDirectoryNotFoundPath(logger, path);
            yield break;
        }

        foreach (var dll in Directory.GetFiles(path, "*.dll"))
        {
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

            yield return assembly;
        }
    }

    [LoggerMessage(LogLevel.Warning, "Plugin directory not found: {Path}", EventId = 0)]
    static partial void LogPluginDirectoryNotFoundPath(ILogger<AssemblyLoader> logger, string Path);

    [LoggerMessage(LogLevel.Warning, "Failed to load assembly {Dll}", EventId = 1)]
    static partial void LogFailedToLoadAssemblyDll(ILogger<AssemblyLoader> logger, string Dll, Exception ex);
}