﻿//---------------------------------------------------
// <auto-generated>
//      This code was auto-generated by FastRPC.Proxy.Generator, version 0.0.1+253467b3b4b5365ef9a37c7beb8d673057c8c7f4.
//
//      Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------
using System.CodeDom.Compiler;
using FastRPC.Communication;
using FastRPC.Communication.Core;

namespace FastRPC.Proxy.Generated
{
    /// <inheritdoc />
    [GeneratedCode("FastRPC.Proxy.Generator", "0.0.1+253467b3b4b5365ef9a37c7beb8d673057c8c7f4")]
    internal sealed class EngineControls : CefForUnity.Shared.Core.IEngineControls
    {
        private readonly Client client;
           
        /// <summary>
        ///     Creates a new <see cref="EngineControls"/> instance
        /// </summary>
        public EngineControls(Client client)
        {
            this.client = client;
        }
		/// <inheritdoc />
		public CefForUnity.Shared.Events.PixelsEvent GetPixels()
		{
			object[] returnObjects = client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.GetPixels");
			return (CefForUnity.Shared.Events.PixelsEvent)returnObjects[0];
		}
		
		/// <inheritdoc />
		public void Shutdown()
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.Shutdown");
		}
		
		/// <inheritdoc />
		public void SendKeyboardEvent(CefForUnity.Shared.Events.KeyboardEvent @keyboardEvent)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.SendKeyboardEvent", new object[] {@keyboardEvent});
		}
		
		/// <inheritdoc />
		public void SendMouseMoveEvent(CefForUnity.Shared.Events.MouseScrollEvent @mouseMoveEvent)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.SendMouseMoveEvent", new object[] {@mouseMoveEvent});
		}
		
		/// <inheritdoc />
		public void SendMouseClickEvent(CefForUnity.Shared.Events.MouseClickEvent @mouseClickEvent)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.SendMouseClickEvent", new object[] {@mouseClickEvent});
		}
		
		/// <inheritdoc />
		public void SendMouseScrollEvent(CefForUnity.Shared.Events.MouseScrollEvent @mouseScrollEvent)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.SendMouseScrollEvent", new object[] {@mouseScrollEvent});
		}
		
		/// <inheritdoc />
		public System.Numerics.Vector2 GetScrollPosition()
		{
			object[] returnObjects = client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.GetScrollPosition");
			return (System.Numerics.Vector2)returnObjects[0];
		}
		
		/// <inheritdoc />
		public void GoForward()
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.GoForward");
		}
		
		/// <inheritdoc />
		public void GoBack()
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.GoBack");
		}
		
		/// <inheritdoc />
		public void Refresh()
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.Refresh");
		}
		
		/// <inheritdoc />
		public void LoadUrl(string @url)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.LoadUrl", new object[] {@url});
		}
		
		/// <inheritdoc />
		public void LoadHtml(string @html)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.LoadHtml", new object[] {@html});
		}
		
		/// <inheritdoc />
		public void ExecuteJS(string @js)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.ExecuteJS", new object[] {@js});
		}
		
		/// <inheritdoc />
		public void Resize(CefForUnity.Shared.Resolution @resolution)
		{
			client.InvokeMethod("CefForUnity.Shared.Core.IEngineControls.Resize", new object[] {@resolution});
		}
    }
}