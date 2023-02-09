// Cef(Chromium Engine Framework) For Unity

using System.Numerics;
using CefForUnity.Shared.Events;
using FastRPC.Proxy;

namespace CefForUnity.Shared.Core;

[GenerateProxy(GeneratedName = "EngineControls", GeneratedNamespace = "CefForUnity.Shared.Core")]
internal interface IEngineControls
{
    public PixelsEvent GetPixels();

    public void Shutdown();
    public void SendKeyboardEvent(KeyboardEvent keyboardEvent);
    public void SendMouseMoveEvent(MouseScrollEvent mouseMoveEvent);
    public void SendMouseClickEvent(MouseClickEvent mouseClickEvent);
    public void SendMouseScrollEvent(MouseScrollEvent mouseScrollEvent);
    public Vector2 GetScrollPosition();
    public void GoForward();
    public void GoBack();
    public void Refresh();
    public void LoadUrl(string url);
    public void LoadHtml(string html);
    public void ExecuteJS(string js);
    public void Resize(Resolution resolution);

}