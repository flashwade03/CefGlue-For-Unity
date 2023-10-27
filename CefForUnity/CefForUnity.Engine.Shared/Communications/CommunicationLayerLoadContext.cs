// Cef(Chromium Engine Framework) For Unity

using System.Reflection;
using System.Runtime.Loader;

namespace CefForUnity.Engine.Shared.Communications;

internal class CommunicationLayerLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver resolver;

    public CommunicationLayerLoadContext(string dllPath)
    {
        resolver = new AssemblyDependencyResolver(dllPath);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
        return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
    }
}