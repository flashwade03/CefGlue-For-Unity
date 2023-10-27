// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Shared;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefRequestHandler : CefRequestHandler
{
    private readonly ProxySettings proxySettings;

    public CFUCefRequestHandler(ProxySettings proxySettings)
    {
        this.proxySettings = proxySettings;
    }
    
    protected override CefResourceRequestHandler GetResourceRequestHandler(CefBrowser browser, CefFrame frame, CefRequest request,
        bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
    {
        return null;
    }

    protected override bool GetAuthCredentials(CefBrowser browser, string originUrl, bool isProxy, string host, int port, string realm,
        string scheme, CefAuthCallback callback)
    {
        if(isProxy) callback.Continue(proxySettings.Username, proxySettings.Password);
        return base.GetAuthCredentials(browser, originUrl, isProxy, host, port, realm, scheme, callback);
    }
}