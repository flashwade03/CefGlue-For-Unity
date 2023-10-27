// Cef(Chromium Engine Framework) For Unity

using System.Linq;
using CefForUnity.Engine.Cef.Core;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefLoadHandler : CefLoadHandler
{
    private readonly CFUCefClient client;
    private readonly string[] ignoredLoadUrls = {"about:blank"};

    internal CFUCefLoadHandler(CFUCefClient client)
    {
        this.client = client;
    }

    protected override void OnLoadStart(CefBrowser browser, CefFrame frame, CefTransitionType transitionType)
    {
        string url = frame.Url;
        if (ignoredLoadUrls.Contains(url))
            return;
        
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Load start: {url}");
        
        if(frame.IsMain) client.ClientControls.LoadStart(url);
    }

    protected override void OnLoadEnd(CefBrowser browser, CefFrame frame, int httpStatusCode)
    {
        string url = frame.Url;
        if (ignoredLoadUrls.Contains(url))
            return;
        
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Load end: {url}");
        
        if(frame.IsMain) client.ClientControls.LoadFinish(url);
    }

    protected override void OnLoadError(CefBrowser browser, CefFrame frame, CefErrorCode errorCode,
        string errorText, string failedUrl)
    {
        CefLoggerWrapper.Error(
            $"{CefLoggerWrapper.ConsoleMessageTag} An error occurred while trying to load '{failedUrl}'! Error details: {errorText} (Code: {errorCode})");

        if (errorCode is CefErrorCode.Aborted
            or CefErrorCode.BLOCKED_BY_RESPONSE
            or CefErrorCode.BLOCKED_BY_CLIENT
            or CefErrorCode.BLOCKED_BY_CSP)
            return;
        
        string html = 
            $@"<style>
@import url('https://fonts.googleapis.com/css2?family=Ubuntu&display=swap');
body {{
font-family: 'Ubuntu', sans-serif;
}}
</style>
<h2>An error occurred while trying to load '{failedUrl}'!</h2>
<p>Error: {errorText}<br>(Code: {(int) errorCode})</p>";
        client.LoadHTML(html);
    }
}