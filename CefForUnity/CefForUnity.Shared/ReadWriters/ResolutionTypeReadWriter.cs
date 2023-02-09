// Cef(Chromium Engine Framework) For Unity

using System;
using FastRPC.Extensions.IO;
using FastRPC.IO;
using FastRPC.Types;

namespace CefForUnity.Shared.ReadWriters;

internal sealed class ResolutionTypeReadWriter : TypeReadWriter<Resolution>
{
    public override Resolution Read(BufferedReader reader)
    {
        return new Resolution(reader.ReadUint(), reader.ReadUint());
    }

    public override void Write(BufferedWriter writer, Resolution value)
    {
        writer.WriteUInt(value.Width);
        writer.WriteUInt(value.Height);
    }
}