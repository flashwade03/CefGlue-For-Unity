// Cef(Chromium Engine Framework) For Unity

using System;
using System.Numerics;
using CefForUnity.Engine.Shared.Core;
using CefForUnity.Engine.Shared.Popups;
using CefForUnity.Shared;
using CefForUnity.Shared.Events;
using CefForUnity.Shared.Popups;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefClient : CefClient, IDisposable
{
    public readonly ClientControlsActions ClientControls;

    private readonly CFUCefContextMenuHandler contextMenuHandler;
    private readonly CFUCefDisplayHandler displayHandler;
    private readonly CFUCefLifespanHandler lifespanHandler;

    private readonly CFUCefLoadHandler loadHandler;
    private readonly CFUCefRenderHandler renderHandler;
    private readonly CFUCefRequestHandler requestHandler;

    private CefBrowser browser;
    private CefBrowserHost browserHost;

    public CFUCefClient(CefSize size, PopupAction popupAction, EnginePopupManager popupManager,
        ProxySettings proxySettings, ClientControlsActions clientControlsActions)
    {
        ClientControls = clientControlsActions;

        loadHandler = new CFUCefLoadHandler(this);
        renderHandler = new CFUCefRenderHandler(size);
        lifespanHandler = new CFUCefLifespanHandler(popupAction, popupManager, proxySettings);
        lifespanHandler.AfterCreated += cefBrowser =>
        {
            browser = cefBrowser;
            browserHost = cefBrowser.GetHost();
        };
        displayHandler = new CFUCefDisplayHandler(this);
        requestHandler = new CFUCefRequestHandler(proxySettings);
        contextMenuHandler = new CFUCefContextMenuHandler();
    }

    public void Dispose()
    {
        browserHost?.CloseBrowser(true);
        browserHost?.Dispose();
        GC.SuppressFinalize(this);
    }

    public byte[] GetPixels()
    {
        if (browserHost == null)
            return Array.Empty<byte>();

        return renderHandler.Pixels;
    }
    
    protected override CefLoadHandler GetLoadHandler()
    {
        return loadHandler;
    }

    protected override CefRenderHandler GetRenderHandler()
    {
        return renderHandler;
    }

    protected override CefLifeSpanHandler GetLifeSpanHandler()
    {
        return lifespanHandler;
    }

    protected override CefDisplayHandler GetDisplayHandler()
    {
        return displayHandler;
    }

    protected override CefRequestHandler GetRequestHandler()
    {
        return requestHandler;
    }

    protected override CefContextMenuHandler GetContextMenuHandler()
    {
        return contextMenuHandler;
    }

    public void ProcessKeyboardEvent(KeyboardEvent keyboardEvent)
    {
        foreach (WindowsKey i in keyboardEvent.KeysDown)
        {
            KeyEvent(new CefKeyEvent
            {
                WindowsKeyCode = (int)i,
                EventType = CefKeyEventType.KeyDown
            });
        }

        foreach (WindowsKey i in keyboardEvent.KeysUp)
        {
            KeyEvent(new CefKeyEvent
            {
                WindowsKeyCode = (int)i,
                EventType = CefKeyEventType.KeyUp
            });
        }

        foreach (char c in keyboardEvent.Chars)
        {
            KeyEvent(new CefKeyEvent
            {
#if WINDOWS
                WindowsKeyCode = c,
#else
                Character = c,
#endif
                EventType = CefKeyEventType.Char
            });
        }
    }

    public void ProcessMouseMoveEvent(MouseMoveEvent mouseEvent)
    {
        MouseMoveEvent(new CefMouseEvent
        {
            X = mouseEvent.MouseX,
            Y = mouseEvent.MouseY
        });
    }

    public void ProcessMouseClickEvent(MouseClickEvent mouseClickEvent)
    {
        MouseClickEvent(new CefMouseEvent
        {
            X = mouseClickEvent.MouseX,
            Y = mouseClickEvent.MouseY
        }, mouseClickEvent.MouseClickCount,
            (CefMouseButtonType) mouseClickEvent.MouseClickType,
            mouseClickEvent.MouseEventType == MouseEventType.Up);
    }

    public void ProcessMouseScrollEvent(MouseScrollEvent mouseScrollEvent)
    {
        MouseScrollEvent(new CefMouseEvent
        {
            X = mouseScrollEvent.MouseX,
            Y = mouseScrollEvent.MouseY
        }, mouseScrollEvent.MouseScroll);
    }

    private void KeyEvent(CefKeyEvent keyEvent)
    {
        browserHost.SendKeyEvent(keyEvent);
    }

    private void MouseMoveEvent(CefMouseEvent mouseEvent)
    {
        browserHost.SendMouseMoveEvent(mouseEvent, false);
    }

    private void MouseClickEvent(CefMouseEvent mouseEvent, int clickCount, CefMouseButtonType button, bool mouseUp)
    {
        browserHost.SendMouseClickEvent(mouseEvent, button, mouseUp, clickCount);
    }

    private void MouseScrollEvent(CefMouseEvent mouseEvent, int scroll)
    {
        browserHost.SendMouseWheelEvent(mouseEvent, 0, scroll);
    }

    public void LoadURL(string url)
    {
        browser.GetMainFrame()?.LoadUrl(url);
    }

    public Vector2 GetMouseScrollPosition()
    {
        return renderHandler.MouseScrollPosition;
    }

    public void LoadHTML(string html)
    {
        browser.GetMainFrame()?.LoadUrl($"data:text/html,{html}");
    }

    public void ExecuteJS(string js)
    {
        browser.GetMainFrame()?.ExecuteJavaScript(js, "", 0);
    }

    public void GoBack()
    {
        if(browser.CanGoForward)
            browser.GoForward();
    }

    public void GoForward()
    {
        if(browser.CanGoForward)
            browser.GoForward();
    }

    public void Refresh()
    {
        browser.Reload();
    }
    
    public void Resize(Resolution resolution)
    {
        renderHandler.Resize(new CefSize((int) resolution.Width, (int) resolution.Height));
        browserHost.WasResized();
    }
}