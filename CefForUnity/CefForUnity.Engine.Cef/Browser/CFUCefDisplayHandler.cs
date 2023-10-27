// Cef(Chromium Engine Framework) For Unity

using System;
using CefForUnity.Engine.Cef.Core;
using CefForUnity.Engine.Shared.Core;
using Serilog.Core.Enrichers;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class CFUCefDisplayHandler : CefDisplayHandler
{
    private readonly ClientControlsActions clientControls;

    public CFUCefDisplayHandler(CFUCefClient client)
    {
        this.clientControls = client.ClientControls;
    }

    protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} URL change: {url}");
        clientControls.UrlChange(url);
    }

    protected override void OnFullscreenModeChange(CefBrowser browser, bool fullscreen)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Fullscreen change: {fullscreen}");
        clientControls.FullScreen(fullscreen);
    }

    protected override void OnTitleChange(CefBrowser browser, string title)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Title change: {title}");
        clientControls.TitleChange(title);
    }

    protected override void OnLoadingProgressChange(CefBrowser browser, double progress)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Progress change: {progress}");
        clientControls.ProgressChange(progress);
    }

    protected override bool OnConsoleMessage(CefBrowser browser, CefLogSeverity level, string message,
        string source, int line)
    {
        switch (level)
        {
            case CefLogSeverity.Disable:
                break;
            case CefLogSeverity.Default:
            case CefLogSeverity.Info:
                CefLoggerWrapper.Info($"{CefLoggerWrapper.ConsoleMessageTag} {message}");
                break;
            case CefLogSeverity.Warning:
                CefLoggerWrapper.Warn($"{CefLoggerWrapper.ConsoleMessageTag} {message}");
                break;
            case CefLogSeverity.Error:
            case CefLogSeverity.Fatal:
                CefLoggerWrapper.Error($"{CefLoggerWrapper.ConsoleMessageTag} {message}");
                break;
            case CefLogSeverity.Verbose:
                CefLoggerWrapper.Debug($"{CefLoggerWrapper.ConsoleMessageTag} {message}");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
        return true;
    }
}