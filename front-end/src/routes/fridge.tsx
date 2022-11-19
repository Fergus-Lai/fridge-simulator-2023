import { Unity, useUnityContext } from "react-unity-webgl";

export function Fridge() {
  const { addEventListener, unityProvider, UNSAFE__unityInstance } = useUnityContext({
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

  });

  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
