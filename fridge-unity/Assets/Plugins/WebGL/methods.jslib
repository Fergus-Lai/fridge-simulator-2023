function CallStringFunc(f, str)
{
    try {
        window.dispatchReactUnityEvent(f, UTF8ToString(str));
    } catch (e) {
        console.warn("Failed to dispatch event " + e);
    }
}

mergeInto(LibraryManager.library, {
    AddItem: (data) => CallStringFunc("AddItem", data),
    RemoveItem: (data) => CallStringFunc("RemoveItem", data),
});