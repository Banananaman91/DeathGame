using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    [SerializeField] private Button[] _itemPlace;                       //creates an array of Buttons
    [SerializeField] public GameObject[] _items;                       //creates an array of GameObjects
    [SerializeField] private ManagerScript _manager;                    //reference to ManagerScript

    // Use this for initialization
    void Start () {
        for (int i = 0; i < _items.Length; i++)              //loop which goes through all the items GameObjects                                           
        {
            _itemPlace[i].gameObject.SetActive(false);       //turns off buttons in inventory at the start of the game
        }
    }
    public void AddItem(GameObject newItem)                                                           //function that adds item into inventory (requires a GameObject)
    {
        ItemPickUp item = newItem.GetComponent<ItemPickUp>();
        Sprite sprite = newItem.GetComponent<SpriteRenderer>().sprite;
        _items[item._itemPlace] = newItem;
        _itemPlace[item._itemPlace].gameObject.SetActive(true);
        _itemPlace[item._itemPlace].image.overrideSprite = sprite;
        _itemPlace[item._itemPlace].onClick.AddListener(() => _manager.LoadInfo(item._itemPlace));
    }
}
