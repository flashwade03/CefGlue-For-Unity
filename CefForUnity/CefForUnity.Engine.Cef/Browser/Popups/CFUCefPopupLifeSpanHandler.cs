// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Engine.Cef.Core;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser.Popups;

public class CFUCefPopupLifeSpanHandler : CefLifeSpanHandler
{
    private readonly Action onShutdown;
    private CefBrowser cefBrowser;
    
    public CFUCefPopupLifeSpanHandler(Action onShutdown)
    {
        this.onShutdown = onShutdown;
    }

    public void Close()
    {
        cefBrowser?.GetHost().CloseBrowser();
    }

    public void ExecuteJS(string js)
    {
        cefBrowser?.GetMainFrame()?.ExecuteJavaScript(js, "", 0);
    }

    protected override void OnAfterCreated(CefBrowser browser)
    {
        cefBrowser = browser;
    }

    protected override void OnBeforeClose(CefBrowser browser)
    {
        CefLoggerWrapper.Debug("Closing popup...");
        onShutdown.Invoke();
    }
}