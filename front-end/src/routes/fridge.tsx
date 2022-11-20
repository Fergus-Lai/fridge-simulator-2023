import { useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";

function sendMsg(obj : string, func : string, arg : string)
{
  (window as any).unityInstance.SendMessage(obj, func, arg);
}

export function Fridge() {
  const { addEventListener, unityProvider } = useUnityContext({
    loaderUrl: "unity/Build.loader.js",
    dataUrl: "unity/Build.data",
    frameworkUrl: "unity/Build.framework.js",
    codeUrl: "unity/Build.wasm",
  });

  useEffect(() => 
  {
    addEventListener("AddItem", (data) => {
    try {
      console.log(data);
    }
    catch (e) {console.error(e);}
    console.log();
    setTimeout(() => sendMsg("Main Camera", "OnTestMessage", "hello!!"), 2000);
  })
    
  });
  
  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
