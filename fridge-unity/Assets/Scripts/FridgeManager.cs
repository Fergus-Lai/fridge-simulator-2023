using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

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

    [SerializeField] private GameObject addUI;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private TMP_Dropdown typeDropdown;
    [SerializeField] private TMP_InputField inputFieldType;

    private FoodType[] types;
    private FoodType unknownType;

    void Awake()
    {
        Instance = this;
        shelves = new();
        foreach (var s in shelfList) { shelves.Add(s.shelfName, s); }
    }

    void Start()
    {
        this.types = Resources.LoadAll<FoodType>("Food/").ToArray();
        this.unknownType = Resources.Load<FoodType>("Food/Generic");
        #if UNITY_EDITOR
        string testStr = "{\"items\":[{\"id\":\"013279823u\",\"name\":\"Milk!!\",\"type\":\"milk\",\"shelf\":\"top\"},{\"id\":\"013279823u\",\"name\":\"Milk 2!!\",\"type\":\"milk\",\"shelf\":\"top\"},{\"id\":\"013279823u\",\"name\":\"idk!!\",\"type\":\"ooba\",\"shelf\":\"top\"},{\"id\":\"013279823u\",\"name\":\"Milk Soup!!\",\"type\":\"milk\",\"shelf\":\"top\"}]}";
        AddItems(testStr);
        #endif

        this.shelfUI.SetActive(false);
        this.typeDropdown.ClearOptions();
        this.typeDropdown.AddOptions(this.types.Select(f => f.id).ToList());
        this.typeDropdown.onValueChanged.AddListener(TypeDropdownChanged);
        this.AddUICancel();
    }

    private void TypeDropdownChanged(int value)
    {
        this.inputFieldType.gameObject.SetActive(types[value] == unknownType);
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
        this.AddUICancel();
    }

    public void AddButtonClicked()
    {
        this.addUI.SetActive(true);
    }

    public void AddUICancel()
    {
        this.inputFieldName.text = "";
        this.inputFieldType.text = "";
        this.typeDropdown.value = 0;
        this.addUI.SetActive(false);
    }

    public void AddUISubmit()
    {
        string name = inputFieldName.text;
        var type = types[this.typeDropdown.value];
        string typeName = type == unknownType ? this.inputFieldType.text : type.id;
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(typeName) || selectedShelf == null) return;

        Guid guid = Guid.NewGuid();
        var item = new ItemSerialised()
        {
            id = guid.ToString(),
            name = name,
            type = typeName,
            shelf = selectedShelf.name,
        };
        AddItem(item);
        JSBindings.AddItem(item);

        AddUICancel();
    }

    public void AddItems(string itemsJson) // called from JS
    {
        var items = JsonUtility.FromJson<SerialisedItems>(itemsJson).items;
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(ItemSerialised item)
    {
        FoodType type = unknownType;
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
