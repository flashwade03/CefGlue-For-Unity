// Cef(Chromium Engine Framework) For Unity

using System;

namespace CefForUnity.Shared.Events;

public struct PixelsEvent
{
    public ReadOnlyMemory<byte> PixelData { get; set; }
}