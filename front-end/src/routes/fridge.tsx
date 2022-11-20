import { useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";

const url : string = "http://localhost:3000/";

function sendMsg(obj : string, func : string, arg : string)
{
  (window as any).unityInstance.SendMessage(obj, func, arg);
}

async function removeItem(itemId : string)
{
  console.log("Removing " + itemId);
  let resp = await fetch(url + "fridge",
  {
    method: "DEL",
    body: JSON.stringify({id: itemId}),
  });

  if (!resp.ok) console.log(await resp.text());
}

async function addItem(name : string, type : string, id : string, shelf : string, date : string)
{
  console.log("Adding item " + name + " type " + type + " id " + id + " shelf " + shelf + " date " + date);
  let resp = await fetch(url + "fridge",
  {
    method: "POST",
    body: JSON.stringify({name: name, id: id, type: type, expDate: date, quantity: 1}),
  });

  if (!resp.ok) console.log(await resp.text());
}

async function init(addEventListener : any)
{
  let resp = await fetch(url + "fridges");

  if (resp.ok)
  {
    let dat = await resp.json();
    let data = {items: dat.fridge};
    sendMsg("fridge", "AddItems", JSON.stringify(data));
  }

  addEventListener("AddItem", (data : any) => {
    let dat = JSON.parse(data);
    addItem(dat.name, dat.type, dat.id, dat.shelf, dat.date);
  });

  addEventListener("RemoveItem", (data : any) => {
    removeItem(data);
  });
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

    init(addEventListener);
    
  });
  
  return <Unity unityProvider={unityProvider} className="unity-canvas" />;
}
