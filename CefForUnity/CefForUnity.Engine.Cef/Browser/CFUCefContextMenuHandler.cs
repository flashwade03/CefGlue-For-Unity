// Cef(Chromium Engine Framework) For Unity

using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefContextMenuHandler : CefContextMenuHandler
{
    protected override bool RunContextMenu(CefBrowser browser, CefFrame frame, CefContextMenuParams parameters,
        CefMenuModel model,
        CefRunContextMenuCallback callback)
    {
        return true;
    }
}