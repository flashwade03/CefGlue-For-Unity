// Cef(Chromium Engine Framework) For Unity

using System;

namespace CefForUnity.Shared.Popups;

public abstract class EnginePopupInfo : IDisposable
{
    public readonly Guid PopupGuid;
    
    protected EnginePopupInfo()
    {
        PopupGuid = Guid.NewGuid();
    }

    internal EnginePopupInfo(Guid guid)
    {
        PopupGuid = guid;
    }

    public abstract void ExecuteJS(string js);
    
    public virtual void Dispose()
    {
    }
}