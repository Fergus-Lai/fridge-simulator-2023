using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FridgeThingyScript : MonoBehaviour, IPointerClickHandler
{
    public string foodName;
    public string id;
    public string typeName;
    public string date;

    public void OnPointerClick(PointerEventData eventData)
    {
        FridgeManager.Instance.SelectItem(this);
    }

    public void Remove()
    {
        JSBindings.RemoveItemById(this.id);
        Destroy(this.gameObject);
    }

    public void Init(FridgeManager.ItemSerialised item)
    {
        this.foodName = item.name;
        this.typeName = item.type;
        this.id = item.id;
        this.date = item.expDate;
    }
}
