import { Unity, useUnityContext } from "react-unity-webgl";

export function Fridge() {
  const { unityProvider } = useUnityContext({
    loaderUrl: "unity/Build.loader.js",
    dataUrl: "unity/Build.data",
    frameworkUrl: "unity/Build.framework.js",
    codeUrl: "unity/Build.wasm",
  });

  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
