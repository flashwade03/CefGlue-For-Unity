// Cef(Chromium Engine Framework) For Unity

using System;

namespace CefForUnity.Shared;

[Serializable]
public struct Resolution
{
    public uint Width;
    public uint Height;
    
    public Resolution(uint width, uint height)
    {
        Width = width;
        Height = height;
    }

    public override string ToString() => $"{Width} x {Height}";

    public override bool Equals(object obj)
    {
        if (obj is Resolution resolution)
        {
            Equals(resolution);
        }

        return false;
    }

    public bool Equals(Resolution other)
    {
        return Width == other.Width && Height == other.Height;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height);
    }
}