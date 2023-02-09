// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Shared.Events;
using FastRPC.Extensions.IO;
using FastRPC.IO;
using FastRPC.Types;

namespace CefForUnity.Shared.ReadWriters;

internal sealed class MouseMoveEventTypeReadWriter : TypeReadWriter<MouseMoveEvent>
{
    public override MouseMoveEvent Read(BufferedReader reader)
    {
        return new MouseMoveEvent
        {
            MouseX = reader.ReadInt(),
            MouseY = reader.ReadInt()
        };
    }

    public override void Write(BufferedWriter writer, MouseMoveEvent value)
    {
        writer.WriteInt(value.MouseX);
        writer.WriteInt(value.MouseY);
    }
}