


var createUnityInstanceReal = createUnityInstance;
createUnityInstance = function(e,t,n) { 
    var inst = createUnityInstanceReal(e, t, n);
    inst.then((x) => {window.unityInstance = x;});
    return inst;
}