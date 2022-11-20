


var createUnityInstanceReal = createUnityInstance;
window.onInstanceCreate = (i) => {};
createUnityInstance = function(e,t,n) { 
    var inst = createUnityInstanceReal(e, t, n);
    inst.then((x) => {window.unityInstance = x; window.onInstanceCreate(x); });
    return inst;
}