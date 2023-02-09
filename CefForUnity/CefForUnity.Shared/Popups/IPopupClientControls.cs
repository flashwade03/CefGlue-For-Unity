// Cef(Chromium Engine Framework) For Unity

using System;
using FastRPC.Proxy;

namespace CefForUnity.Shared.Popups;

[GenerateProxy(GeneratedName = "PopupClientControls", GeneratedNamespace = "CefForUnity.Shared.Popups")]
internal interface IPopupClientControls
{
    public void PopupClose(Guid guid);
    public void PopupExecuteJS(Guid guid, string js);
}