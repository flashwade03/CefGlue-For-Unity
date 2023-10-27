// Cef(Chromium Engine Framework) For Unity

using System;
using System.Runtime.InteropServices;

namespace CefForUnity.Engine.Cef.Core;

public class CefSafeBuffer : SafeBuffer
{
    public CefSafeBuffer(IntPtr data, ulong noOfBytes) : base(false)
    {
        SetHandle(data);
        Initialize(noOfBytes);
    }

    protected override bool ReleaseHandle()
    {
        return true;
    }
}