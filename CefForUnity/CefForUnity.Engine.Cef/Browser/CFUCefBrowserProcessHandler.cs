// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Engine.Cef.Browser.Popups;
using CefForUnity.Engine.Cef.Browser.Schemes;
using CefForUnity.Engine.Shared.Pages;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefBrowserProcessHandler : CefBrowserProcessHandler, IDisposable
{
    private readonly PageResourceScheme aboutPage;

    public CFUCefBrowserProcessHandler()
    {
        aboutPage = new PageResourceScheme(PagesHandler.GetAbountPageCode());
    }
    
    public void Dispose()
    {
        aboutPage?.Dispose();
        GC.SuppressFinalize(this);
    }

    protected override void OnContextInitialized()
    {
        CefRuntime.RegisterSchemeHandlerFactory("cfu", "about", aboutPage);
    }
}