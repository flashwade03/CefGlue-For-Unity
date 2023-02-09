// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Shared.Events;
using FastRPC.Extensions.IO;
using FastRPC.IO;
using FastRPC.Types;

namespace CefForUnity.Shared.ReadWriters;

internal sealed class MouseClickEventTypeReadWriter : TypeReadWriter<MouseClickEvent>
{
    public override MouseClickEvent Read(BufferedReader reader)
    {
        return new MouseClickEvent
        {
            MouseX = reader.ReadInt(),
            MouseY = reader.ReadInt(),
            MouseClickCount = reader.ReadInt(),
            MouseClickType = (MouseClickType) reader.ReadByte(),
            MouseEventType = (MouseEventType) reader.ReadByte()
        };
    }

    public override void Write(BufferedWriter writer, MouseClickEvent value)
    {
        writer.WriteInt(value.MouseX);
        writer.WriteInt(value.MouseY);
        writer.WriteInt(value.MouseClickCount);
        writer.WriteByte((byte)value.MouseClickType);
        writer.WriteByte((byte)value.MouseEventType);
    }
}