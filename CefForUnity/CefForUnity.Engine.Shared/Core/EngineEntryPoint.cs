// Cef(Chromium Engine Framework) For Unity

using System;
using System.CommandLine;
using System.IO;
using System.Threading;
using CefForUnity.Engine.Shared.Communications;
using CefForUnity.Engine.Shared.Core.Logging;
using CefForUnity.Engine.Shared.Popups;
using CefForUnity.Engine.Shared.ReadWriters;
using CefForUnity.Shared;
using CefForUnity.Shared.Communications;
using CefForUnity.Shared.Core;
using CefForUnity.Shared.Popups;
using FastRPC.Communication.Common.Exception;
using FastRPC.Communication.Core;

namespace CefForUnity.Engine.Shared.Core;

public abstract class EngineEntryPoint : IDisposable
{
    private Client ipcClient;
    private Host ipcHost;
    
    protected ClientControlsActions ClientControlsActions { get; private set; }
    
    protected EnginePopupManager PopupManager { get; private set; }

    protected bool IsConnected => ipcClient.IsConnected;

    protected abstract bool ShouldInitLogger(LaunchArguments launchArguments, string[] args);

    protected abstract void EarlyInit(LaunchArguments launchArguments, string[] args);

    protected abstract void EntryPoint(LaunchArguments launchArguments, string[] args);

    public int Main(string[] args)
    {
        Option<string> initialUrl = new("-initial-url", () => "https://111percent.net",
            "The initial URL that the browser will first load to.");

        Option<int> width = new("-width",
            () => 1080,
            "The width of the window");
        Option<int> height = new("-height",
            () => 1920,
            "The height of the window");

        Option<bool> javaScript = new("-javascript",
            () => true,
            "Enable or disable javascript");
        Option<bool> webRtc = new("-web-rtc",
            () => false,
            "Enable or disable web RTC");
        Option<bool> localStorage = new("-local-storage",
            () => true,
            "Enable or disable local storage");
        Option<int> remoteDebugging = new("-remote-debugging",
            () => 0,
            "If the engine has remote debugging, what port to use (0 for disable");
        Option<FileInfo> cachePath = new("-cache-path",
            () => null,
            "The path to the cache (null for no cache)");
        Option<PopupAction> popupAction = new("-popup-action",
            () => PopupAction.Ignore,
            "What action to take when dealing with a popup");


        Option<string> backgroundColor = new("-background-color",
            () => "ffffffff",
            "The color to use for the background");

        Option<bool> proxyServer = new("-proxy-server",
            () => true,
            "Use a proxy server or direct connect");
        Option<string> proxyUsername = new("-proxy-username",
            ()=>null,
            "The username to use in the proxy auth");
        Option<string> proxyPassword = new("-proxy-password",
            () => null,
            "The password to use in the proxy auth");

        Option<FileInfo> logPath = new("-log-path",
            () => new FileInfo("engine.log"),
            "The path to where the log file will be");
        Option<LogSeverity> logSeverity = new("-log-severity",
            () => LogSeverity.Info,
            "The severity of the logs");

        Option<FileInfo> communicationLayerPath = new("-comms-layer-path",
            () => null,
            "The location of where the dll for the communication layer is. If none is provided then the in-built TCP layer will be used.");
        Option<string> inLocation = new("-in-location",
            () => "5555",
            "In location for IPC(Pipes location or TCP port in TCP mode)");
        Option<string> outLocation = new("-out-location",
            () => "5556",
            "Out location for IPC(Pipes location or TCP port in TCP mode)");

        Option<uint> startDelay = new("-start-delay",
            () => 0,
            "Delays the starting process. Used for testing reasons.");

        RootCommand rootCommand = new()
        {
            initialUrl,
            width, height,
            javaScript, webRtc, localStorage, remoteDebugging, cachePath, popupAction,
            backgroundColor,
            proxyServer, proxyUsername, proxyPassword,
            logPath, logSeverity,
            communicationLayerPath, inLocation, outLocation,
            startDelay
        };
        rootCommand.Description = "Cef For Unity Engine";
        rootCommand.TreatUnmatchedTokensAsErrors = false;

        LaunchArgumentsBinder launchArgumentsBinder = new(
            initialUrl,
            width, height,
            javaScript, webRtc, localStorage, remoteDebugging, cachePath, popupAction,
            backgroundColor,
            proxyServer, proxyUsername, proxyPassword,
            logPath, logSeverity,
            communicationLayerPath, inLocation, outLocation,
            startDelay);
        rootCommand.SetHandler(parsedArgs =>
        {
            if(parsedArgs.StartDelay != 0)
                Thread.Sleep((int)parsedArgs.StartDelay);

            if (ShouldInitLogger(parsedArgs, args))
                Logger.Init(parsedArgs.LogSeverity);

            ClientControlsActions = new ClientControlsActions();
            PopupManager = new EnginePopupManager();

            try
            {
                EarlyInit(parsedArgs, args);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"{Logger.BaseLoggingTag}: Uncaught exception occured in early init!");
                ShutdownAndExitWithError();
                return;
            }

            try
            {
                EntryPoint(parsedArgs, args);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"{Logger.BaseLoggingTag}: Uncaught exception occured in the entry point!");
                ShutdownAndExitWithError();
                return;
            }
            
            Logger.Shutdown();
        }, launchArgumentsBinder);
        
        return rootCommand.Invoke(args);
    }

    internal void SetupIpc(IEngineControls engineControls, LaunchArguments arguments)
    {
        try
        {
            ICommunicationLayer communicationLayer;
            if (arguments.CommunicationLayerPath == null)
            {
                //Use TCP
                Logger.Debug($"{Logger.BaseLoggingTag}: No communication layer provided, using default TCP...");
                communicationLayer = new TCPCommunicationLayer();
            }
            else
            {
                communicationLayer = CommunicationLayerLoader.GetCommunicationLayerFromAssembly(
                    arguments.CommunicationLayerPath.FullName);
            }

            try
            {
                ipcHost = communicationLayer.CreateHost(arguments.InLocation);
                ipcClient = communicationLayer.CreateClient(arguments.OutLocation);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"{Logger.BaseLoggingTag}: An error occured setting up the communication layer!");
                ShutdownAndExitWithError();
                return;
            }

            //Add type readers
            EngineReadWriterManager.AddTypeReadWriterManager(ipcHost.TypeReadWriterManager);
            ipcHost.AddService(typeof(IEngineControls), engineControls);
            ipcHost.AddService(typeof(IPopupClientControls), PopupManager);
            ipcHost.StartListeningAsync().ConfigureAwait(false);

            EngineReadWriterManager.AddTypeReadWriterManager(ipcClient.TypeReadWriterManager);
            ipcClient.AddService(typeof(IClientControls));
            ipcClient.AddService(typeof(IPopupEngineControls));

            //Connect the engine (us) back to Unity
            try
            {
                ipcClient.Connect();
                
                ClientControlsActions.SetIpcClient(ipcClient);
                PopupManager.SetIpcClient(ipcClient);
            }
            catch (ConnectionFailedException)
            {
                Logger.Error(
                    $"{Logger.BaseLoggingTag}: The engine failed to connect back to the Unity client! Client events will not fire!");
                ipcClient.Dispose();
                ipcClient = null;
            }

            Logger.Debug($"{Logger.BaseLoggingTag}: IPC Setup done.");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"{Logger.BaseLoggingTag}: Error setting up IPC!");
        }
    }

    protected void Ready()
    {
        ClientControlsActions.Ready();
    }

    private void ShutdownAndExitWithError()
    {
        Dispose();
        Logger.Shutdown();
        Environment.Exit(-1);
    }
    
    #region Dispose

    ~EngineEntryPoint()
    {
        ReleaseResources();
    }
    
    public void Dispose()
    {
        ReleaseResources();
        GC.SuppressFinalize(this);
    }

    protected virtual void ReleaseResources()
    {
        ClientControlsActions.Dispose();
        ipcHost?.Dispose();
    }
    #endregion
}