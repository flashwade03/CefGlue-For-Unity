using System;
using System.CommandLine;
using System.CommandLine.Binding;
using CefGlueForUnity.Shared;
using CefGlueForUnity.Engine.Shared;

namespace CefGlueForUnity.Engine.Shared.Core;

public class LaunchArgumentsBinder : BinderBase<LaunchArguments>
{
	private readonly Option<string> initialUrl;
	private readonly Option<int> width;
	private readonly Option<int> height;
	private readonly Option<bool> javaScript;
	private readonly Option<bool> localStorage;
	private readonly Option<FileInfo> cachePath;

	private readonly Option<string> backgroundColor;

	private readonly Option<FileInfo> logPath;
	private readonly Option<LogSeverity> logSeverity;

	public LaunchArgumentsBinder(
		Option<string> initialUrl,
		Option<int> width, Option<int> height,
		Option<bool> javaScript, Option<bool> localStorage, Option<FileInfo> cachePath,
		Option<string> backgroundColor,
		Option<FileInfo> logPath, Option<LogSeverity> logSeverity)
	{
		this.initialUrl = initialUrl;
		this.width = width;
		this.height = height;

		this.javaScript = javaScript;
		this.localStorage = localStorage;
		this.cachePath = cachePath;

		this.backgroundColor = backgroundColor;

		this.logPath = logPath;
		this.logSeverity = logSeverity;
	}

    protected override LaunchArguments GetBoundValue(BindingContext bindingContext)
    {
		return new LaunchArguments
		{
			InitialUrl = bindingContext.ParseResult.GetValueForOption(initialUrl),
			Width = bindingContext.ParseResult.GetValueForOption(width),
			Height = bindingContext.ParseResult.GetValueForOption(height),

			JavaScript = bindingContext.ParseResult.GetValueForOption(javaScript),
			LocalStorage = bindingContext.ParseResult.GetValueForOption(localStorage),
			CachePath = bindingContext.ParseResult.GetValueForOption(cachePath),

			BackgroundColor = new Color(bindingContext.ParseResult.GetValueForOption(backgroundColor)),

			LogPath = bindingContext.ParseResult.GetValueForOption(logPath),
			LogSeverity = bindingContext.ParseResult.GetValueForOption(logSeverity)
		};
    }
}

