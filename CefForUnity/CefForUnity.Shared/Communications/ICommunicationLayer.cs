// Cef(Chromium Engine Framework) For Unity

using FastRPC.Communication.Core;

namespace CefForUnity.Shared.Communications;

public interface ICommunicationLayer
{
    public Client CreateClient(string location);
    public Host CreateHost(string location);
}