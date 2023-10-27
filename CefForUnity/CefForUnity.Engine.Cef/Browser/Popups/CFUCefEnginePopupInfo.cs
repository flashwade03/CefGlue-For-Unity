// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Engine.Cef.Core;
using CefForUnity.Engine.Shared.Popups;
using CefForUnity.Shared;
using CefForUnity.Shared.Popups;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser.Popups;

public class CFUCefEnginePopupInfo : EnginePopupInfo
{
    private readonly EnginePopupManager popupManager;
    private readonly CFUCefPopupClient popupClient;

    public CFUCefEnginePopupInfo(EnginePopupManager popupManager, ProxySettings proxySettings, ref CefClient client)
    {
        this.popupManager = popupManager;

        popupClient = new CFUCefPopupClient(proxySettings, DisposeNoClose);
        client = popupClient;
    }
    
    public override void ExecuteJS(string js)
    {
        throw new System.NotImplementedException();
    }

    public override void Dispose()
    {
        if (!CefRuntime.CurrentlyOn(CefThreadId.UI))
        {
            CefEngineControlsManager.PostTask(CefThreadId.UI, Dispose);
            return;
        }
        
        base.Dispose();
        popupClient.Close();
    }
    
    private void DisposeNoClose()
    {
        popupManager.OnPopupDestroy(this);
    }
}