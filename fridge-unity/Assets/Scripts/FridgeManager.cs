using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    public static FridgeManager Instance { get; private set; }

    public List<ShelfScript> shelfList;
    private Dictionary<string, ShelfScript> shelves;
    private ShelfScript selectedShelf;
    [SerializeField]
    private KeepBoxInFrame cameraController;

    [SerializeField]
    private GameObject cameraTarget;

    [SerializeField]
    private GameObject shelfUI;

    void Awake()
    {
        Instance = this;
        shelves = new();
        foreach (var s in shelfList) { shelves.Add(s.shelfName, s); }
    }

    void Start()
    {
        #if UNITY_EDITOR
        string testStr = "{\"items\":[{\"id\":\"013279823u\",\"name\":\"Milk!!\",\"type\":\"milk\",\"shelf\":\"top\"},{\"id\":\"013279823u\",\"name\":\"Milk 2!!\",\"type\":\"milk\",\"shelf\":\"top\"},{\"id\":\"013279823u\",\"name\":\"idk!!\",\"type\":\"ooba\",\"shelf\":\"top\"},{\"id\":\"013279823u\",\"name\":\"Milk Soup!!\",\"type\":\"milk\",\"shelf\":\"top\"}]}";
        AddItems(testStr);
        #endif

        this.shelfUI.SetActive(false);
    }

    public void ShelfClicked(ShelfScript shelf)
    {
        if (selectedShelf == null)
        {
            selectedShelf = shelf;
            shelf.IsFocused = true;
            cameraController.direction = ViewDirection.Top;
            cameraController.target = shelf.gameObject;
            this.shelfUI.SetActive(true);
        }
    }

    public void ReturnButtonClicked()
    {
        if (selectedShelf != null)
        {
            selectedShelf.IsFocused = false;
        }

        selectedShelf = null;
        cameraController.direction = ViewDirection.Front;
        cameraController.target = cameraTarget;
        this.shelfUI.SetActive(false);
    }

    public void AddButtonClicked()
    {
        
    }

    public void AddItems(string itemsJson) // called from JS
    {
        var types = Resources.LoadAll<FoodType>("Food/");
        var defaultFood = Resources.Load<FoodType>("Food/Generic");
        var items = JsonUtility.FromJson<SerialisedItems>(itemsJson).items;
        foreach (var item in items)
        {
            FoodType type = defaultFood;
            foreach (var t in types)
            {
                if (item.type.Contains(t.id, StringComparison.OrdinalIgnoreCase))
                {
                    type = t;
                    break;
                }
            }

            var go = Instantiate(type.prefab);
            var shelf = shelves.GetValueOrDefault(item.shelf, null) ?? shelves.Values.First();
            go.transform.SetParent(shelf.transform);
            shelf.AlignChildren();
        }
    }

    [Serializable]
    public struct SerialisedItems
    {
        public ItemSerialised[] items;
    }

    [Serializable]
    public struct ItemSerialised
    {
        public string id;
        public string name;
        public string type;
        public string shelf;
    }
}
