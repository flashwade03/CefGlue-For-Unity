// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Shared.Events;
using FastRPC.Extensions.IO;
using FastRPC.IO;
using FastRPC.Types;

namespace CefForUnity.Shared.ReadWriters;

internal sealed class MouseScrollEventTypeReadWriter : TypeReadWriter<MouseScrollEvent>
{
    public override MouseScrollEvent Read(BufferedReader reader)
    {
        return new MouseScrollEvent
        {
            MouseX = reader.ReadInt(),
            MouseY = reader.ReadInt(),
            MouseScroll = reader.ReadInt()
        };
    }

    public override void Write(BufferedWriter writer, MouseScrollEvent value)
    {
        writer.WriteInt(value.MouseX);
        writer.WriteInt(value.MouseY);
        writer.WriteInt(value.MouseScroll);
    }
}