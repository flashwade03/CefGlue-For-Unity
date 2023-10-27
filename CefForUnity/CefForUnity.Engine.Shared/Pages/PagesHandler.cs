// Cef(Chromium Engine Framework) For Unity

using System.IO;
using System.Reflection;

namespace CefForUnity.Engine.Shared.Pages;

public static class PagesHandler
{
    private static readonly Assembly EngineSharedAssembly = typeof(PagesHandler).Assembly;

    public static Stream GetAbountPageCode()
    {
        //Fixme
        Stream stream = EngineSharedAssembly.GetManifestResourceStream("asdlkfjsdf");
        return stream;
    }
}