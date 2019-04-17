using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

    public Button[] itemPlace;                       //creates an array of Buttons
    public GameObject[] items;                       //creates an array of GameObjects
    public ManagerScript manager;                    //reference to ManagerScript
    GameObject JackieBoy;

    // Use this for initialization
    void Start () {
        JackieBoy = GameObject.FindGameObjectWithTag("Jack");
        for (int i = 0; i < items.Length; i++)              //loop which goes through all the items GameObjects                                           
        {
            itemPlace[i].gameObject.SetActive(false);       //turns off buttons in inventory at the start of the game
        }
    }
	
	// Update is called once per frame
	void Update () {
	        
	}

    public void AddItem(GameObject _item)                                                           //function that adds item into inventory (requires a GameObject)
    {
        for(int i = 0; i < items.Length-1;i++)                                                        //loop which goes through the array and all the items GameObjects
        {
            if (_item == JackieBoy)
            {
                items[items.Length - 1] = _item;                                                                   //assign the item to which ItemPickUp script is assigned to items array of GameObjects
                itemPlace[items.Length - 1].gameObject.SetActive(true);                                            //activates buttons when item is picked up
                itemPlace[items.Length - 1].image.overrideSprite = _item.GetComponent<SpriteRenderer>().sprite;    //change the image of the button to the sprite of item that is being picked up
                itemPlace[items.Length - 1].onClick.AddListener(() => manager.LoadInfo(items.Length - 1));
            }
            else if (items[i] == null)                                                                   //if an items GameObject array is empty, then proceed with code
            {
                items[i] = _item;                                                                   //assign the item to which ItemPickUp script is assigned to items array of GameObjects
                itemPlace[i].gameObject.SetActive(true);                                            //activates buttons when item is picked up
                itemPlace[i].image.overrideSprite = _item.GetComponent<SpriteRenderer>().sprite;    //change the image of the button to the sprite of item that is being picked up
                itemPlace[i].onClick.AddListener(() => manager.LoadInfo(i));                        //adds a listener to each button which on click goes into ManagerScript and loads function LoadInfo()
                break;                                                                              //break out of the if statement so the loop doesn't fill the whole inventory
            }
        }
    }
}
