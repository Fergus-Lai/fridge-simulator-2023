mergeInto(LibraryManager.library, {
    AddItem: function (data) {
        try {
            window.dispatchReactUnityEvent("AddItem", UTF8ToString(data));
        } catch (e) {
            console.warn("Failed to dispatch event " + e);
        } 
    },});

mergeInto(LibraryManager.library, {
    RemoveItem: function (data) {
        try {
            window.dispatchReactUnityEvent("RemoveItem", UTF8ToString(data));
        } catch (e) {
            console.warn("Failed to dispatch event " + e);
        } 
    },});