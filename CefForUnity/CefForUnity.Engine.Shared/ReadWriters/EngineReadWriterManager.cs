// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Shared.ReadWriters;
using FastRPC.Types;

namespace CefForUnity.Engine.Shared.ReadWriters;

public static class EngineReadWriterManager
{
    public static void AddTypeReadWriterManager(TypeReadWriterManager readWriterManager)
    {
        ReadWriterUtils.AddBaseTypeReadWriters(readWriterManager);
        readWriterManager.AddReadWriter(new PixelsEventTypeReadWriter());
    }
}