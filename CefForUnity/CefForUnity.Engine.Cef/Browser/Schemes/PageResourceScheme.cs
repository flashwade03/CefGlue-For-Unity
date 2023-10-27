// Cef(Chromium Engine Framework) For Unity

using System;
using System.IO;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser.Schemes;

public sealed class PageResourceScheme : CefSchemeHandlerFactory, IDisposable
{
    private readonly string mimeType;
    private readonly Stream stream;

    public PageResourceScheme(Stream stream, string mimeType = StreamCefResourceHandler.DefaultMimeType)
    {
        this.stream = stream;
        this.mimeType = mimeType;
    }
    
    public void Dispose()
    {
        stream.Dispose();
    }

    protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
    {
        return new StreamCefResourceHandler(mimeType, stream);
    }
}