using System;
namespace CefGlueForUnity.Engine.Shared.Core;

public static class Converter
{
	public static byte HexToDecimal(string hex)
	{
		byte dec = Convert.ToByte(hex, 16);
		return dec;
	}
}

