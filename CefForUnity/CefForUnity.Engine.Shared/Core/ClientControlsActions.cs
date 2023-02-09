// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Shared.Core;
using FastRPC.Communication.Core;

namespace CefForUnity.Engine.Shared.Core;

public class ClientControlsActions : IClientControls, IDisposable
{
    private Client? client;
    private IClientControls? clientActions;
    
    public void UrlChange(string url)
    {
        if(client is {IsConnected: true})
            clientActions?.UrlChange(url);
    }

    public void LoadStart(string url)
    {
        throw new NotImplementedException();
    }

    public void LoadFinish(string url)
    {
        throw new NotImplementedException();
    }

    public void TitleChange(string title)
    {
        throw new NotImplementedException();
    }

    public void ProgressChange(double progress)
    {
        throw new NotImplementedException();
    }

    public void FullScreen(bool fullScreen)
    {
        throw new NotImplementedException();
    }

    public void Ready()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}