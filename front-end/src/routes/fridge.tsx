import { Unity, useUnityContext } from "react-unity-webgl";

export function Fridge() {
  const { addEventListener, sendMessage, unityProvider,  } = useUnityContext({
    loaderUrl: "unity/Build.loader.js",
    dataUrl: "unity/Build.data",
    frameworkUrl: "unity/Build.framework.js",
    codeUrl: "unity/Build.wasm",
  });

  addEventListener("AddItem", (data) => {
    try {
    console.log(data);
    }
    catch (e) {console.error(e);}

    setTimeout(() => sendMessage("Main Camera", "OnTestMessage", "hello!!"), 2000);

  });

  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
