using System;
using Boo.Lang;
using InventoryScripts;
using MovementNEW;
using Newtonsoft.Json;
using UnityEngine;

namespace Saving
{
    public class SaveHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pickUps;
        [SerializeField] private InventoryScript _inventoryScript;
        private List<int> _inventoryObjects = new List<int>();
        private static PlayerMovement PlayerMovement => FindObjectOfType<PlayerMovement>();
        private Vector3 PlayerPosition
        {
            get => PlayerMovement.transform.position;
            set => PlayerMovement.transform.position = value;
        }
        private static Quaternion PlayerRotation
        {
            get => PlayerMovement.transform.rotation;
            set => PlayerMovement.transform.rotation = value;
        }
        
        private void Awake()
        {
            SaveSystem.Initialize();
        }

        private void Start()
        {
            Load();
            LoadInventory();
        }

        public void Save()
        {
            var playerPos = PlayerPosition;
            var playerRot = PlayerRotation;
            foreach (var item in _inventoryScript.Items)
            {
                if (item == null) continue;
                _inventoryObjects.Add(item.GetInstanceID());
            }
            var saveData = new SaveData
            {
                playerPosition = playerPos,
                playerRotation = playerRot,
            };
            var json = JsonUtility.ToJson(saveData);
            SaveSystem.Save(json);
        }

        public void SaveInventory()
        {
            var inventoryList = new InventoryList(_inventoryObjects);
            var json = JsonConvert.SerializeObject(inventoryList);
            SaveSystem.SaveInventory(json);
        }
        
        public void Load()
        {
            var saveString = SaveSystem.Load();
            if (saveString != null)
            {
                var saveData = JsonUtility.FromJson<SaveData>(saveString);
                PlayerPosition = saveData.playerPosition;
                PlayerRotation = saveData.playerRotation;
            }
            else
            {
                Debug.Log("No save to load");
            }
        }

        public void LoadInventory()
        {
            var saveString = SaveSystem.LoadInventory();
            if (saveString != null)
            {
                var inventoryData = JsonConvert.DeserializeObject<InventoryList>(saveString);
                var list = inventoryData.dataList;
                foreach (var item in list)
                {
                    CompareItems(item);
                }
            }
            else
            {
                Debug.Log("No inventory to load");
            }
        }

        public void DeleteSave()
        {
            SaveSystem.Delete();
        }

        private void CompareItems(int item)
        {
            foreach (var pickUp in _pickUps)
            {
                if(pickUp.GetInstanceID() != item) continue;
                _inventoryScript.AddItem(pickUp.GetComponent<ItemPickUp>());
                return;
            }
        }
        
        [Serializable]
        private class SaveData
        {
            public Vector3 playerPosition;
            public Quaternion playerRotation;
        }

        [Serializable]
        public struct InventoryList
        {
            public List<int> dataList;

            public InventoryList(List<int> _dataList)
            {
                dataList = _dataList;
            }
        }
        //Should be improved in the future (making one Json hold all the data)
    }
}
