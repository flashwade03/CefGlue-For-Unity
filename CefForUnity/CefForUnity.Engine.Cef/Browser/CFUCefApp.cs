// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Engine.Shared.Core;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefApp : CefApp
{
    private readonly bool mediaStreamingEnabled;
    private readonly bool noProxyServer;

    private CFUCefBrowserProcessHandler browserProcessHandler;

    public CFUCefApp(LaunchArguments launchArguments)
    {
        mediaStreamingEnabled = launchArguments.WebRtc;
        noProxyServer = !launchArguments.ProxyEnabled;
    }
    
    protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
    {
        if(noProxyServer && !commandLine.HasSwitch("--no-proxy-server"))
            commandLine.AppendSwitch("--no-proxy-server");
        
        if(mediaStreamingEnabled && !commandLine.HasSwitch("--enable-media-stream"))
            commandLine.AppendSwitch("--enable-media-stream");
        
#if MACOS
        if(!commandLine.HasSwitch("--no-zygote")) commandLine.AppendSwitch("--no-zygote");
#endif
    }

    protected override CefBrowserProcessHandler GetBrowserProcessHandler()
    {
        browserProcessHandler = new CFUCefBrowserProcessHandler();
        return browserProcessHandler;
    }

    protected override CefRenderProcessHandler GetRenderProcessHandler()
    {
        return new CFUCefRenderProcessHandler();
    }

    protected override void Dispose(bool disposing)
    {
        browserProcessHandler.Dispose();
        base.Dispose(disposing);
    }
}