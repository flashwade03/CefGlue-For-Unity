// Cef(Chromium Engine Framework) For Unity
#nullable enable
using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using CefForUnity.Engine.Cef.Core;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Browser;

public class StreamCefResourceHandler : CefResourceHandler
{
    public const string DefaultMimeType = "text/html";
    private byte[] tempBuffer = Array.Empty<byte>();

    public string? Charset { get; set; }
    public string MimeType { get; set; }
    public Stream Stream { get; set; }
    public int StatusCode { get; set; }
    public string StatusText { get; set; }
    public long? ResponseLength { get; set; }
    public NameValueCollection Headers { get; }
    public bool AutoDisposeStream { get; set; }
    public CefErrorCode? ErrorCode { get; set; }
    
    public StreamCefResourceHandler(string mimeType = DefaultMimeType, Stream? stream = null,
        bool autoDisposeStream = false, string? charset = null)
    {
        if (string.IsNullOrEmpty(mimeType)) throw new ArgumentNullException(nameof(mimeType));

        StatusCode = 200;
        StatusText = "OK";
        MimeType = mimeType;
        Headers = new NameValueCollection();
        Stream = stream ?? Stream.Null;
        AutoDisposeStream = autoDisposeStream;
        Charset = charset;
        
        Headers.Add("Access-Control-Allow-Origin", "*");
    }

    public virtual CefReturnValue ProcessRequestAsync(CefRequest request, CefCallback callback)
    {
        return CefReturnValue.Continue;
    }

    public static byte[] GetByteArray(string text, Encoding encoding, bool includePreamble = true)
    {
        if (includePreamble)
        {
            byte[] preamble = encoding.GetPreamble();
            byte[] bytes = encoding.GetBytes(text);

            MemoryStream memoryStream = new MemoryStream(preamble.Length + bytes.Length);
            
            memoryStream.Write(preamble,0,preamble.Length);
            memoryStream.Write(bytes, 0, bytes.Length);

            memoryStream.Position = 0;

            return memoryStream.ToArray();
        }

        return encoding.GetBytes(text);
    }

    public virtual void Dispose()
    {
        if(AutoDisposeStream && Stream is not null) Stream.Dispose();

        Stream = Stream.Null;
        tempBuffer = Array.Empty<byte>();
    }
    
    protected override bool Open(CefRequest request, out bool handleRequest, CefCallback callback)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Stream Cef Resource Handler Open() called.");
        CefReturnValue processRequest = ProcessRequestAsync(request, callback);

        if (processRequest == CefReturnValue.ContinueAsync)
        {
            handleRequest = false;
            return true;
        }

        if (processRequest == CefReturnValue.Continue)
        {
            handleRequest = true;
            return true;
        }

        handleRequest = true;
        return false;
    }

    protected override void GetResponseHeaders(CefResponse response, out long responseLength, out string redirectUrl)
    {
        CefLoggerWrapper.Debug(
            $"{CefLoggerWrapper.FullCefMessageTag} Stream Cef Resource Handler GetResponseHeaders() called.");
        redirectUrl = string.Empty;
        responseLength = -1;

        response.MimeType = MimeType;
        response.Status = StatusCode;
        response.StatusText = StatusText;
        response.SetHeaderMap(Headers);

        if (!string.IsNullOrEmpty(Charset)) response.Charset = Charset ?? string.Empty;

        if (ResponseLength.HasValue) responseLength = ResponseLength.Value;

        if (Stream is not null && Stream.CanSeek)
        {
            if (ResponseLength is null || responseLength == 0)
                responseLength = Stream.Length;

            Stream.Position = 0;
        }
    }

    protected override bool Skip(long bytesToSkip, out long bytesSkipped, CefResourceSkipCallback callback)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Stream Cef Resource Handler Skip() called.");
        if (Stream is null || !Stream.CanSeek)
        {
            bytesSkipped = -2;
            return false;
        }

        bytesSkipped = bytesToSkip;
        Stream.Seek(bytesToSkip, SeekOrigin.Current);
        return true;
    }

    protected override bool Read(IntPtr dataOut, int bytesToRead, out int bytesRead, CefResourceReadCallback callback)
    {
        CefLoggerWrapper.Debug($"{CefLoggerWrapper.FullCefMessageTag} Stream Cef Resource Handler Read() called.");
        bytesRead = 0;
        
        callback.Dispose();

        if (Stream is null) return false;

        if (tempBuffer is null || tempBuffer.Length < bytesToRead) tempBuffer = new byte[bytesToRead];

        bytesRead = Stream.Read(tempBuffer, 0, bytesToRead);

        if (bytesRead == 0) return false;
        
        using (CefSafeBuffer safeBuffer = new CefSafeBuffer(dataOut, (ulong)bytesRead))
        using (UnmanagedMemoryStream dataOutStream =
               new UnmanagedMemoryStream(safeBuffer, 0, bytesRead, FileAccess.Write))
        {
            dataOutStream.Write(tempBuffer, 0, bytesRead);
        }

        return bytesRead > 0;
    }

    protected override void Cancel()
    {
    }

    protected override void Dispose(bool disposing)
    {
        Dispose();
        base.Dispose(disposing);
    }
}