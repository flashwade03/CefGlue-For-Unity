// Cef(Chromium Engine Framework) For Unity

using System;
using Xilium.CefGlue;

namespace CefForUnity.Engine.Cef.Core;

public class CefActionTask : CefTask
{
    private Action action;

    public CefActionTask(Action action)
    {
        this.action = action;
    }

    protected override void Execute()
    {
        action();
        action = null;
    }
}