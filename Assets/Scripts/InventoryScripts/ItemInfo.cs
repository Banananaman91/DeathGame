using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour {

    [SerializeField] private string _itemName;
    [SerializeField] private string _itemDescription;
    public string ItemName => _itemName;
    public string ItemDescription => _itemDescription;

}
