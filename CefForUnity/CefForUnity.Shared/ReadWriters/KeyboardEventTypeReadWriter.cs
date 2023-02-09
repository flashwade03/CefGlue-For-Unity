// Cef(Chromium Engine Framework) For Unity

using CefForUnity.Shared.Events;
using FastRPC.Extensions.IO;
using FastRPC.IO;
using FastRPC.Types;

namespace CefForUnity.Shared.ReadWriters;

internal sealed class KeyboardEventTypeReadWriter : TypeReadWriter<KeyboardEvent>
{
    public override KeyboardEvent Read(BufferedReader reader)
    {
        return new KeyboardEvent
        {
            Chars = ReadChars(reader),
            KeysDown = ReadKeys(reader),
            KeysUp = ReadKeys(reader)
        };
    }

    public override void Write(BufferedWriter writer, KeyboardEvent value)
    {
        WriteChars(writer, value.Chars);
        WriteKeys(writer, value.KeysDown);
        WriteKeys(writer, value.KeysUp);
    }

    private void WriteChars(BufferedWriter writer, char[] chars)
    {
        writer.WriteInt(chars.Length);
        for (int i = 0; i < chars.Length; ++i)
        {
            writer.WriteChar(chars[i]);
        }
    }
    
    private char[] ReadChars(BufferedReader reader)
    {
        char[] chars = new char[reader.ReadInt()];
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = reader.ReadChar();
        }

        return chars;
    }

    private void WriteKeys(BufferedWriter writer, WindowsKey[] keys)
    {
        writer.WriteInt(keys.Length);

        foreach (WindowsKey key in keys) writer.WriteInt((int) key);
    }

    private WindowsKey[] ReadKeys(BufferedReader reader)
    {
        WindowsKey[] keys = new WindowsKey[reader.ReadInt()];
        for (int i = 0; i < keys.Length; i++) keys[i] = (WindowsKey) reader.ReadInt();

        return keys;
    }
}