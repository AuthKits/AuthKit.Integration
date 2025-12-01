using Microsoft.Extensions.Logging;

namespace Plugins.Features.AssemblyLoading;

public partial class AssemblyLoader
{
    [LoggerMessage(LogLevel.Warning, "Plugin directory not found: {path}", EventId = 0)]
    static partial void LogPluginDirectoryNotFoundPath(ILogger<AssemblyLoader> logger, string path);

    [LoggerMessage(LogLevel.Warning, "Failed to load assembly {dll}", EventId = 1)]
    static partial void LogFailedToLoadAssemblyDll(ILogger<AssemblyLoader> logger, string dll, Exception ex);

    [LoggerMessage(LogLevel.Warning, "DLL file not found: {dllPath}", EventId = 2)]
    static partial void LogDllNotFound(ILogger<AssemblyLoader> logger, string dllPath);
    
    [LoggerMessage(LogLevel.Debug, "Successfully loaded assembly: {dll}")]
    static partial void LogSuccessfullyLoadedAssemblyDll(ILogger<AssemblyLoader> logger, string dll);
}