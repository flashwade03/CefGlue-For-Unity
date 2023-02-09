// Cef(Chromium Engine Framework) For Unity

using System.CodeDom.Compiler;
using FastRPC.Proxy;

namespace CefForUnity.Shared.Core;

[GenerateProxy(GeneratedName = "ClientControls", GeneratedNamespace = "CefForUnity.Shared.Core")]
internal interface IClientControls
{
    public void UrlChange(string url);
    public void LoadStart(string url);
    public void LoadFinish(string url);
    public void TitleChange(string title);
    public void ProgressChange(double progress);
    public void FullScreen(bool fullScreen);
    public void Ready();
}
