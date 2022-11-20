using System;
using System.Runtime.InteropServices;

public static class JSBindings
{
    [DllImport("__Internal")]
    private static extern void AddItem(string data);
    [DllImport("__Internal")]
    public static extern void RemoveItem(string id);

    public static void AddItem(FridgeManager.ItemSerialised item)
    {
        #if !UNITY_EDITOR
        AddItem(UnityEngine.JsonUtility.ToJson(item));
        #endif
    }
}