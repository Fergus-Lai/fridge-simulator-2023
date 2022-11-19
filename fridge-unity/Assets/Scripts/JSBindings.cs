using System;
using System.Runtime.InteropServices;

public static class JSBindings
{
    [DllImport("__Internal")]
    public static extern void AddItem(string data);
}