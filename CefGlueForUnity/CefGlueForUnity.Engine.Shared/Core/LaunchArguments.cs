using System;
using CefGlueForUnity.Shared;

namespace CefGlueForUnity.Engine.Shared.Core;

public class LaunchArguments
{
	public string InitialUrl { get; set; }
	public int Width { get; init; }
	public int Height { get; init; }
	public bool JavaScript { get; init; }
	public bool LocalStorage { get; init; }
	public Color BackgroundColor { get; init; }
	public FileInfo CachePath { get; init; }
	public FileInfo LogPath { get; init; }
	public LogSeverity LogSeverity { get; init; }

}


