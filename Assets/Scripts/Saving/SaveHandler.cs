﻿using System;
using System.Collections.Generic;
using Interaction;
using InventoryScripts;
using MovementNEW;
using Newtonsoft.Json;
using Pages;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Saving
{
    public class SaveHandler : MonoBehaviour
    {
        [Serializable]
        private struct PlayerData
        {
            public Vector3 playerPosition;
            public Quaternion playerRotation;
        }

        [Serializable]
        public struct InventoryList
        {
            public List<int> inventoryList;
            public List<int> pageList;

            public InventoryList(List<int> _inventoryList, List<int> _pageList)
            {
                inventoryList = _inventoryList;
                pageList = _pageList;
            }
        }
        
        [Serializable]
        public struct DialogueSerialize
        {
            public List<int> dialogueKey;
            public List<Dialogue> dialogueScriptable;

            public DialogueSerialize(List<int> dK, List<Dialogue> dS)
            {
                dialogueKey = dK;
                dialogueScriptable = dS;
            }
        }
        
        [SerializeField] private InventoryScript _inventoryScript;
        private readonly List<int> _inventoryObjects = new List<int>();
        private readonly List<int> _inventoryPages = new List<int>();
        private static IEnumerable<ItemPickUp> PickUps => Resources.FindObjectsOfTypeAll<ItemPickUp>();
        private static IEnumerable<Page> Pages => Resources.FindObjectsOfTypeAll<Page>();
        private static IEnumerable<FurnitureInteract> Interactables => FindObjectsOfType<FurnitureInteract>();
        private static PlayerMovement PlayerMovement => FindObjectOfType<PlayerMovement>();
        private static Vector3 PlayerPosition
        {
            get => PlayerMovement.transform.position;
            set => PlayerMovement.transform.position = value;
        }
        private static Quaternion PlayerRotation
        {
            get => PlayerMovement.transform.rotation;
            set => PlayerMovement.transform.rotation = value;
        }
        public Dictionary<int, Dialogue> Dialogues { get; } = new Dictionary<int, Dialogue>();

        private void Awake()
        {
            SaveSystem.Initialize();
        }

        private void Start()
        {
            
            LoadGame();
        }

        public void SaveGame()
        {
            SaveSystem.Save(PlayerJson(), InventoryJson(), DialogueJson());
        }

        public void LoadGame()
        {
            var (item1, item2, item3) = SaveSystem.Load();
            if (item1 != null)
            {
                var saveData = JsonUtility.FromJson<PlayerData>(item1);
                PlayerPosition = saveData.playerPosition;
                PlayerRotation = saveData.playerRotation;
            }
            if (item2 != null)
            {
                var inventoryData = JsonConvert.DeserializeObject<InventoryList>(item2);
                var invList = inventoryData.inventoryList;
                var pageList = inventoryData.pageList;
                foreach (var item in invList)
                {
                    CompareItems(item);
                }
                foreach (var page in pageList)
                {
                    AllocatePages(page);
                }
            }
            if (item3 != null)
            {
                var dialogueData = JsonUtility.FromJson<DialogueSerialize>(item3);
                for (int i = 0; i < dialogueData.dialogueKey.Count; i++)
                {
                    PlaceDialogues(dialogueData.dialogueKey[i], dialogueData.dialogueScriptable[i]);
                }
            }
        }
 
        public void DeleteSave()
        {
            SaveSystem.Delete();
        }

        private static string PlayerJson()
        {
            var playerPos = PlayerPosition;
            var playerRot = PlayerRotation;
            var saveData = new PlayerData
            {
                playerPosition = playerPos,
                playerRotation = playerRot,
            };
            var playerJson = JsonUtility.ToJson(saveData);
            return playerJson;
        }

        private string InventoryJson()
        {
            foreach (var item in _inventoryScript.Items)
            {
                if (item == null) continue;
                _inventoryObjects.Add(item.GetInstanceID());
            }

            foreach (var book in _inventoryScript.Pages)
            {
                if (book == null) continue;
                _inventoryPages.Add(book.GetInstanceID());
            }
            
            var inventoryList = new InventoryList(_inventoryObjects, _inventoryPages);
            var inventoryJson = JsonConvert.SerializeObject(inventoryList);
            return inventoryJson;
        }
        
        private string DialogueJson()
        {
            var keyIds = new List<int>();
            var dialogueSciptables = new List<Dialogue>();
            foreach (var dialogue in Dialogues)
            {
                keyIds.Add(dialogue.Key);
                dialogueSciptables.Add(dialogue.Value);
            }
            var saveData = new DialogueSerialize(keyIds, dialogueSciptables);
            return JsonUtility.ToJson(saveData);
        }

        private void CompareItems(int itemId)
        {
            foreach (var pickUp in PickUps)
            {
                if(pickUp.GetInstanceID() != itemId) continue;
                _inventoryScript.AddItem(pickUp.GetComponent<ItemPickUp>());
                return;
            }
        }

        private void AllocatePages(int pageId)
        {
            foreach (var page in Pages)
            {
                if(page.GetInstanceID() != pageId) continue;
                _inventoryScript.AddPage(page);
                return;
            }
        }
        
        private void PlaceDialogues(int objectID, Dialogue dialogue)
        {
            foreach (var interactable in Interactables)
            {
                if(interactable.GetInstanceID() != objectID) continue;
                interactable.SwapDialogue(dialogue);
            }
        }
    }
}
