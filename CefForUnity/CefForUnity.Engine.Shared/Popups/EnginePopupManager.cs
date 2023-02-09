// Cef(Chromium Engine Framework) For Unity

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CefForUnity.Shared.Popups;
using FastRPC.Communication.Core;
using FastRPC.Proxy.Generated;

namespace CefForUnity.Engine.Shared.Popups;

public class EnginePopupManager : IPopupClientControls
{
    private Client? client;
    private IPopupEngineControls? engineControls;

    private readonly List<EnginePopupInfo> popups;

    public EnginePopupManager()
    {
        popups = new List<EnginePopupInfo>();
    }

    public void OnPopup(EnginePopupInfo enginePopupInfo)
    {
        popups.Add(enginePopupInfo);
        if(client is { IsConnected: true})
            engineControls?.OnPopup(enginePopupInfo.PopupGuid);
    }

    public void OnPopupDestroy(EnginePopupInfo enginePopupInfo)
    {
        popups.Remove(enginePopupInfo);
        if(client is {IsConnected: true})
            engineControls?.OnPopupDestroy(enginePopupInfo.PopupGuid);
    }
    
    public void PopupClose(Guid guid)
    {
        EnginePopupInfo popupInfo = popups.First(x => x.PopupGuid == guid);
        popups.Remove(popupInfo);
        _ = Task.Run(popupInfo.Dispose);
    }

    public void PopupExecuteJS(Guid guid, string js)
    {
        EnginePopupInfo popupInfo = popups.First(x => x.PopupGuid == guid);
        popupInfo.ExecuteJS(js);
    }

    internal void SetIpcClient(Client ipcClient)
    {
        client = ipcClient;
        engineControls = new PopupEngineControls(client);
    }
}