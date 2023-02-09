// Cef(Chromium Engine Framework) For Unity

namespace CefForUnity.Shared.Events;

public struct MouseScrollEvent
{
    public int MouseX { get; set; }
    public int MouseY { get; set; }
    public int MouseScroll { get; set; }
}