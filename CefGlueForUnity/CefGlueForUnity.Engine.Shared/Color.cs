using System;
using CefGlueForUnity.Engine.Shared.Core;

namespace CefGlueForUnity.Engine.Shared;

public readonly struct Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public byte A { get; }

    public Color(string hex)
    {
        if (hex.Length < 6)
            throw new ArgumentOutOfRangeException(nameof(hex));

        R = Converter.HexToDecimal(hex.Substring(0, 2));
        G = Converter.HexToDecimal(hex.Substring(2, 2));
        B = Converter.HexToDecimal(hex.Substring(4, 2));

        A = hex.Length >= 8 ? Converter.HexToDecimal(hex.Substring(6, 2)) : (byte)255;
    }

    public override string ToString()
    {
        return $"({R}, {G}, {B}, {A})";
    }
}
