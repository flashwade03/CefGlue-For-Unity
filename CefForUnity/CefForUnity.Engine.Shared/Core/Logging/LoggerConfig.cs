// Cef(Chromium Engine Framework) For Unity

namespace CefForUnity.Engine.Shared.Core.Logging;

public sealed class LoggerConfig
{
    public bool BufferedFileWrite = true;
    public string LogDirectory = "/Logs";
    public string LogFileDateTimeFormat = "yyyy-MM-dd-HH-mm-ss";
}