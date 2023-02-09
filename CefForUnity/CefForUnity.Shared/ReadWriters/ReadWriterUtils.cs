// Cef(Chromium Engine Framework) For Unity

using FastRPC.Types;

namespace CefForUnity.Shared.ReadWriters;

internal static class ReadWriterUtils
{
    public static void AddBaseTypeReadWriters(TypeReadWriterManager readWriterManager)
    {
        readWriterManager.AddReadWriter(new KeyboardEventTypeReadWriter());
        readWriterManager.AddReadWriter(new MouseClickEventTypeReadWriter());
        readWriterManager.AddReadWriter(new MouseMoveEventTypeReadWriter());
        readWriterManager.AddReadWriter(new MouseScrollEventTypeReadWriter());
        readWriterManager.AddReadWriter(new ResolutionTypeReadWriter());
    }
}