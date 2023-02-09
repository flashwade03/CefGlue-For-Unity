// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Engine.Shared.Utils;

namespace CefForUnity.Engine.Shared;

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

        R = HexConverter.HexToDec(hex.Substring(0, 2));
        G = HexConverter.HexToDec(hex.Substring(2, 2));
        B = HexConverter.HexToDec(hex.Substring(4, 2));
        A = hex.Length >= 8 ? HexConverter.HexToDec(hex.Substring(6, 2)) : (byte) 255;
    }

    public override string ToString()
    {
        return $"({R}, {G}, {B}, {A})";
    }
}