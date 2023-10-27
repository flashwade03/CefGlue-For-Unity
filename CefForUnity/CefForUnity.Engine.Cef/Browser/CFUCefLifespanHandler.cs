// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Engine.Cef.Browser.Popups;
using CefForUnity.Engine.Cef.Core;
using CefForUnity.Engine.Shared.Popups;
using CefForUnity.Shared;
using CefForUnity.Shared.Popups;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefLifespanHandler : CefLifeSpanHandler
{
    public event Action<CefBrowser> AfterCreated;

    private readonly EnginePopupManager popupManager;
    private readonly ProxySettings proxySettings;

    private readonly PopupAction popupAction;

    public CFUCefLifespanHandler(PopupAction popupAction, EnginePopupManager enginePopupManager,
        ProxySettings proxySettings)
    {
        this.proxySettings = proxySettings;
        this.popupAction = popupAction;
        popupManager = enginePopupManager;
    }

    protected override void OnAfterCreated(CefBrowser browser)
    {
        AfterCreated?.Invoke(browser);
    }

    protected override bool OnBeforePopup(CefBrowser browser, CefFrame frame, string targetUrl,
        string targetFrameName,
        CefWindowOpenDisposition targetDisposition, bool userGesture, CefPopupFeatures popupFeatures,
        CefWindowInfo windowInfo, ref CefClient client, CefBrowserSettings settings,
        ref CefDictionaryValue extraInfo,
        ref bool noJavascriptAccess)
    {
        CefLoggerWrapper.Debug($"Popup: {targetFrameName}({targetUrl})");

        switch (popupAction)
        {
            case PopupAction.Ignore:
                break;
            case PopupAction.OpenExternalWindow:
                popupManager.OnPopup(new CFUCefEnginePopupInfo(popupManager, proxySettings, ref client));
                return false;
            case PopupAction.Redirect:
                frame.LoadUrl(targetUrl);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }

    protected override bool DoClose(CefBrowser browser)
    {
        return false;
    }
}