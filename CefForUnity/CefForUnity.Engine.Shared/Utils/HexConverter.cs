// Cef(Chromium Engine Framework) For Unity

using System;

namespace CefForUnity.Engine.Shared.Utils;

public static class HexConverter
{
    public static byte HexToDec(string hex)
    {
        byte dec = Convert.ToByte(hex, 16);
        return dec;
    }
}