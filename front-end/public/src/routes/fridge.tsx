import React from "react";
import { Unity, useUnityContext } from "react-unity-webgl";
import { useAuth0 } from "@auth0/auth0-react";

export function Fridge() {
  const { loginWithRedirect, logout, isAuthenticated } = useAuth0();

  const { unityProvider } = useUnityContext({
    loaderUrl: "unity/Build.loader.js",
    dataUrl: "unity/Build.data",
    frameworkUrl: "unity/Build.framework.js",
    codeUrl: "unity/Build.wasm",
  });

  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
