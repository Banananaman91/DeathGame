using MovementNEW;
using UnityEngine;

namespace Saving
{
    public class SaveHandler : MonoBehaviour
    {
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
            Load();
        }

        public void Save()
        {
            var playerPos = PlayerPosition;
            var playerRot = PlayerRotation;
            var saveData = new SaveData
            {
                playerPosition = playerPos,
                playerRotation = playerRot
            };
            var json = JsonUtility.ToJson(saveData);
            SaveSystem.Save(json);
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

        public void DeleteSave()
        {
            SaveSystem.Delete();
        }
        
        private class SaveData
        {
            public Vector3 playerPosition;
            public Quaternion playerRotation;
        }
    }
}
