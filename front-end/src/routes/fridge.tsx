import { useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";

function sendMsg(obj : string, func : string, arg : string)
{
  (window as any).unityInstance.SendMessage(obj, func, arg);
}

function removeItem(itemId : string)
{
  console.log("Removing " + itemId);
}

function addItem(name : string, type : string, id : string, shelf : string)
{
  console.log("Adding item " + name + " type " + type + " id " + id + " shelf " + shelf);
}

export function Fridge() {
  const { addEventListener, unityProvider } = useUnityContext({
    loaderUrl: "unity/Build.loader.js",
    dataUrl: "unity/Build.data",
    frameworkUrl: "unity/Build.framework.js",
    codeUrl: "unity/Build.wasm",
  });

  (window as any).onInstanceCreate = ((i:any) => 
  {
    sendMsg("fridge", "AddItems", JSON.stringify({items: [
      {
        id: "013279823u",
        name: "Milk!!",
        type: "milk",
        shelf: "top",
      },{
        id: "013279823u",
        name: "Milk 2!!",
        type: "milk",
        shelf: "top",
      },{
        id: "013279823u",
        name: "idk!!",
        type: "ooba",
        shelf: "top",
      },{
        id: "013279823u",
        name: "Milk Soup!!",
        type: "milk",
        shelf: "top",
      },
    ]}));

    addEventListener("AddItem", (data) => {
      let dat = JSON.parse(data);
      addItem(dat.name, dat.type, dat.id, dat.shelf);
    });

    addEventListener("RemoveItem", (data) => {
      removeItem(data);
    });
  });
  
  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
