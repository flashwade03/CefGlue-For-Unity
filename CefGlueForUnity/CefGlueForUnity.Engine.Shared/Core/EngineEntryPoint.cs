using System;
namespace CefGlueForUnity.Engine.Shared.Core;

public abstract class EngineEntryPoint : IDisposable
{
	public EngineEntryPoint()
	{
	}


	public void Dispose()
	{
		ReleaseResources();
		GC.SuppressFinalize(this);
	}

	protected virtual void ReleaseResources()
	{

	}
}

