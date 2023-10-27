// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Shared;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser.Popups;

public class CFUCefPopupClient : CefClient
{
    private readonly CFUCefRequestHandler requestHandler;
    private readonly CFUCefPopupLifeSpanHandler lifeSpanHandler;

    public CFUCefPopupClient(ProxySettings proxySettings, Action onShutdown)
    {
        requestHandler = new CFUCefRequestHandler(proxySettings);
        lifeSpanHandler = new CFUCefPopupLifeSpanHandler(onShutdown);
    }

    public void Close()
    {
        lifeSpanHandler.Close();
    }

    public void ExecuteJS(string js)
    {
        lifeSpanHandler.ExecuteJS(js);
    }

    protected override CefRequestHandler GetRequestHandler()
    {
        return requestHandler;
    }

    protected override CefLifeSpanHandler GetLifeSpanHandler()
    {
        return lifeSpanHandler;
    }
}