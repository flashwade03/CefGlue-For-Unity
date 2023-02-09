// Cef(Chromium Engine Framework) For Unity

using System;

namespace CefForUnity.Shared;

public class InitializationException : Exception
{
    public InitializationException()
    {}

    public InitializationException(string message) : base(message)
    {
    }
    
    public InitializationException(string message, Exception inner) : base(message, inner)
    {}
}