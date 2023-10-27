// Cef(Chromium Engine Framework) For Unity

using System;
using System.IO;
using System.Linq;
using System.Numerics;
using CefForUnity.Engine.Cef.Browser;
using CefForUnity.Engine.Cef.Browser.Schemes;
using CefForUnity.Engine.Shared;
using CefForUnity.Engine.Shared.Core;
using CefForUnity.Engine.Shared.Core.Logging;
using CefForUnity.Engine.Shared.Popups;
using CefForUnity.Shared;
using CefForUnity.Shared.Events;
using CefForUnity.Shared.Popups;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Core;

public class CefEngineControlsManager
{
    private string[] args;
    private CFUCefApp cefApp;

    private CFUCefClient cefClient;
    private CefMainArgs cefMainArgs;
    private LaunchArguments launchArguments;

    public CefEngineControlsManager()
    {
        CefRuntime.Load();
    }

    public void EarlyInit(LaunchArguments arguments, string[] rawArguments)
    {
        launchArguments = arguments;
        args = rawArguments;

        string[] argv = args;
#if MACOS
        argv = new string[args.Length +1];
        Array.Copy(args, 0, argv, 1, args.Length);
        argv[0] = "-";
#endif

        cefMainArgs = new CefMainArgs(argv);
        cefApp = new CFUCefApp(launchArguments);

        int exitCode = CefRuntime.ExecuteProcess(cefMainArgs, cefApp, IntPtr.Zero);
        if (exitCode != -1)
        {
            CefLoggerWrapper.Debug("Sub-Process exit: {ExitCode}", exitCode);
            Environment.Exit(exitCode);
            return;
        }

        if (argv.Any(arg => arg.StartsWith("--type=")))
        {
            CefLoggerWrapper.Error("Invalid process type!");
            Environment.Exit(-2);
            throw new Exception("Invalid process type!");
        }
    }

    public void INit(ClientControlsActions clientControlsActions, EnginePopupManager popupManager)
    {
        string cachePathArgument = null;
        if (launchArguments.CachePath != null)
            cachePathArgument = launchArguments.CachePath.FullName;

        CefLogSeverity logSeverity = launchArguments.LogSeverity switch
        {
            LogSeverity.Debug => CefLogSeverity.Debug,
            LogSeverity.Info => CefLogSeverity.Info,
            LogSeverity.Warn => CefLogSeverity.Warning,
            LogSeverity.Error => CefLogSeverity.Error,
            LogSeverity.Fatal => CefLogSeverity.Fatal,
            _ => CefLogSeverity.Default
        };

        CefSettings cefSettings = new()
        {
            WindowlessRenderingEnabled = true,
            NoSandbox = true,
            LogFile = launchArguments.LogPath.FullName,
            CachePath = cachePathArgument,
            MultiThreadedMessageLoop = false,
            LogSeverity = logSeverity,
            Locale = "en-US",
            ExternalMessagePump = false,
            RemoteDebuggingPort = launchArguments.RemoteDebugging,
            PersistSessionCookies = true,
            PersistUserPreferences = true,
#if MACOS
            ResourcesDirPath = Path.Combine(Environment.CurrentDirectory),
            LocalesDirPath = Path.Combine(Environment.CurrentDirectory, "locales"),
            BrowserSubprocessPath = Environment.ProcessPath
#endif
        };
        
        CefRuntime.Initialize(cefMainArgs, cefSettings, cefApp, IntPtr.Zero);

        CefWindowInfo cefWindowInfo = CefWindowInfo.Create();
        cefWindowInfo.SetAsWindowless(IntPtr.Zero, false);

        Color suppliedColor = launchArguments.BackgroundColor;
        CefColor backgroundColor = new(suppliedColor.A, suppliedColor.R, suppliedColor.G, suppliedColor.B);
        CefBrowserSettings cefBrowserSettings = new()
        {
            BackgroundColor = backgroundColor,
            JavaScript = launchArguments.JavaScript ? CefState.Enabled : CefState.Disabled,
            LocalStorage = launchArguments.LocalStorage ? CefState.Enabled : CefState.Disabled
        };
        
        Logger.Debug($"{CefLoggerWrapper.FullCefMessageTag} Starting CEF with these options : "+
                     $"\nProcess Path: {Environment.ProcessPath}" +
                     $"\nJS: {launchArguments.JavaScript}" +
                     $"\nLocal Storage: {launchArguments.LocalStorage}" +
                     $"\nBackgroundColor: {suppliedColor}" +
                     $"\nCache Path: {cachePathArgument}" +
                     $"\nPopup Action: {launchArguments.PopupAction}" + 
                     $"\nLog Path: {launchArguments.LogPath.FullName}" +
                     $"\nLog Severity: {launchArguments.LogSeverity}");
        Logger.Info($"{CefLoggerWrapper.FullCefMessageTag} Starting CEF client...");

        cefClient = new CFUCefClient(new CefSize(launchArguments.Width, launchArguments.Height),
            launchArguments.PopupAction, popupManager,
            new ProxySettings(launchArguments.ProxyUsername, launchArguments.ProxyPassword,
                launchArguments.ProxyEnabled), clientControlsActions);
        CefBrowserHost.CreateBrowser(cefWindowInfo, cefClient, cefBrowserSettings, launchArguments.InitialUrl);
    }

    public static void PostTask(CefThreadId threadId, Action action)
    {
        CefRuntime.PostTask(threadId, new CefActionTask(action));
    }

    public PixelsEvent GetPixels()
    {
        return new PixelsEvent
        {
            PixelData = cefClient.GetPixels()
        };
    }

    public void Shutdown()
    {
        if (!CefRuntime.CurrentlyOn(CefThreadId.UI))
        {
            PostTask(CefThreadId.UI, Shutdown);
            return;
        }
        
        Logger.Debug("Quitting message loop...");
        CefRuntime.QuitMessageLoop();
    }

    public void SendKeyboardEvent(KeyboardEvent keyboardEvent)
    {
        cefClient.ProcessKeyboardEvent(keyboardEvent);
    }

    public void SendMouseMoveEvent(MouseMoveEvent mouseMoveEvent)
    {
        cefClient.ProcessMouseMoveEvent(mouseMoveEvent);
    }

    public void SendMouseClickEvent(MouseClickEvent mouseClickEvent)
    {
        cefClient.ProcessMouseClickEvent(mouseClickEvent);
    }

    public void SendMouseScrollEvent(MouseScrollEvent mouseScrollEvent)
    {
        cefClient.ProcessMouseScrollEvent(mouseScrollEvent);
    }

    public Vector2 GetScrollPosition()
    {
        return cefClient.GetMouseScrollPosition();
    }

    public void GoForward()
    {
        cefClient.GoForward();
    }

    public void GoBack()
    {
        cefClient.GoBack();
    }

    public void Refresh()
    {
        cefClient.Refresh();
    }

    public void LoadUrl(string url)
    {
        cefClient.LoadURL(url);
    }

    public void LoadHtml(string html)
    {
        cefClient.LoadHTML(html);
    }

    public void ExecuteJs(string js)
    {
        cefClient.ExecuteJS(js);
    }

    public void Resize(Resolution resolution)
    {
        cefClient.Resize(resolution);
    }
    
    ~CefEngineControlsManager()
    {
        ReleaseResources();
    }

    public void Dispose()
    {
        ReleaseResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseResources()
    {
        cefClient?.Dispose();
        CefRuntime.Shutdown();
    }
}