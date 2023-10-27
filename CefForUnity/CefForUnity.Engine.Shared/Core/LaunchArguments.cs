// Cef(Chromium Engine Framework) For Unity

using System.IO;
using CefForUnity.Shared;
using CefForUnity.Shared.Popups;

namespace CefForUnity.Engine.Shared.Core;

public class LaunchArguments
{
    public string InitialUrl { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public bool JavaScript { get; init; }
    public bool WebRtc { get; init; }
    public bool LocalStorage { get; init; }
    public PopupAction PopupAction { get; init; }
    public int RemoteDebugging { get; init; }
    public Color BackgroundColor { get; init; }
    public FileInfo CachePath { get; init; }
    public bool ProxyEnabled { get; init; }
    public string ProxyUsername { get; init; }
    public string ProxyPassword { get; init; }
    public FileInfo LogPath { get; init; }
    public LogSeverity LogSeverity { get; init; }
    internal FileInfo CommunicationLayerPath { get; init; }
    internal string InLocation { get; init; }
    internal string OutLocation { get; init; }
    internal uint StartDelay { get; init; }
}