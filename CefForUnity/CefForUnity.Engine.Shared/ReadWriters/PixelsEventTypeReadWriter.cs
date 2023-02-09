// Cef(Chromium Engine Framework) For Unity
#nullable enable

using CefForUnity.Shared.Events;
using FastRPC.Extensions.IO;
using FastRPC.IO;
using FastRPC.Types;

namespace CefForUnity.Engine.Shared.ReadWriters;

public class PixelsEventTypeReadWriter : TypeReadWriter<PixelsEvent>
{
    public override PixelsEvent Read(BufferedReader reader)
    {
        throw new System.NotImplementedException();
    }

    public override void Write(BufferedWriter writer, PixelsEvent value)
    {
        if (value.PixelData.Length <= 0)
        {
            writer.WriteInt(-1);
            return;
        }
        
        writer.WriteInt(value.PixelData.Length);
        
        foreach(byte b in value.PixelData.Span)
            writer.WriteByte(b);
    }
}