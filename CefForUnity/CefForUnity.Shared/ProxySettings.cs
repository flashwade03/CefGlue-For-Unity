// Cef(Chromium Engine Framework) For Unity

using System;

namespace CefForUnity.Shared;

[Serializable]
public struct ProxySettings
{
    public string Username;
    public string Password;
    public bool ProxyServer;

    public ProxySettings(string username, string password, bool proxyServer)
    {
        Username = username;
        Password = password;
        ProxyServer = proxyServer;
    }
}