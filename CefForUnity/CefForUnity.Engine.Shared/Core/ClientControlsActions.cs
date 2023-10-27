// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Shared.Core;
using FastRPC.Communication.Core;
using FastRPC.Proxy.Generated;

namespace CefForUnity.Engine.Shared.Core;

public class ClientControlsActions : IClientControls, IDisposable
{
    private Client? client;
    private IClientControls? clientActions;

    private bool IsConnected => client is {IsConnected: true};
    
    public void UrlChange(string url)
    {
        if(IsConnected)
            clientActions?.UrlChange(url);
    }

    public void LoadStart(string url)
    {
        if(IsConnected)
            clientActions?.LoadStart(url);
    }

    public void LoadFinish(string url)
    {
        if(IsConnected)
            clientActions?.LoadFinish(url);
    }

    public void TitleChange(string title)
    {
        if(IsConnected)
            clientActions?.TitleChange(title);
    }

    public void ProgressChange(double progress)
    {
        if(IsConnected)
            clientActions?.ProgressChange(progress);
    }

    public void FullScreen(bool fullScreen)
    {
        if(IsConnected)
            clientActions?.FullScreen(fullScreen);
    }

    public void Ready()
    {
        if(IsConnected)
            clientActions?.Ready();
    }
    
    internal void SetIpcClient(Client ipcClient)
    {
        client = ipcClient ?? throw new NullReferenceException();
        clientActions = new ClientControls(client);
    }
    
    public void Dispose()
    {
        ReleaseResources();
        GC.SuppressFinalize(this);
    }

    ~ClientControlsActions()
    {
        ReleaseResources();
    }

    private void ReleaseResources()
    {
        client?.Dispose();
    }
}