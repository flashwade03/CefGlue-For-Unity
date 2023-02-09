// Cef(Chromium Engine Framework) For Unity

using System;
using FastRPC.Proxy;

namespace CefForUnity.Shared.Popups;

[GenerateProxy(GeneratedName = "PopupEngineControls", GeneratedNamespace = "CefForUnity.Shared.Popups")]
public interface IPopupEngineControls
{
    public void OnPopup(Guid guid);
    public void OnPopupDestroy(Guid guid);
}