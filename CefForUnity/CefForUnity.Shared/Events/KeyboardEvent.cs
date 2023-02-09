// Cef(Chromium Engine Framework) For Unity

using System.Dynamic;

namespace CefForUnity.Shared.Events;

public struct KeyboardEvent
{
    public WindowsKey[] KeysUp { get; set; }
    public WindowsKey[] KeysDown { get; set; }
    public char[] Chars { get; set; }
}