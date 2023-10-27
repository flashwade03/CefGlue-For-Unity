// Cef(Chromium Engine Framework) For Unity

using System;
using System.Net;
using CefForUnity.Shared.Communications;
using FastRPC.Communication.Core;
using FastRPC.Communication.Protocol.TCP;

namespace CefForUnity.Engine.Shared.Communications;

internal class TCPCommunicationLayer : ICommunicationLayer
{
    public Client CreateClient(string location)
    {
        if (int.TryParse(location, out int port)) return CreateTCPClient(port);

        throw new ArgumentOutOfRangeException(nameof(location));
    }

    public Host CreateHost(string location)
    {
        if (int.TryParse(location, out int port)) return CreateTCPHost(port);

        throw new ArgumentOutOfRangeException(nameof(location));
    }

    /// <summary>
    ///     Creates a new <see cref="TCPClient" />
    /// </summary>
    /// <param name="port"></param>
    /// <returns></returns>
    public Client CreateTCPClient(int port)
    {
        IPEndPoint ipEndPoint = new(IPAddress.Loopback, port);
        return new TCPClient(ipEndPoint);
    }

    /// <summary>
    ///     Creates a new <see cref="TCPHost" />
    /// </summary>
    /// <param name="port"></param>
    /// <returns></returns>
    public Host CreateTCPHost(int port)
    {
        IPEndPoint ipEndPoint = new(IPAddress.Loopback, port);
        return new TCPHost(ipEndPoint);
    }
}