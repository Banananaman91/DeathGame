using UnityEngine;

namespace MovementNEW
{
    public class RoomCameras : MonoBehaviour
    {
        [SerializeField] private GameObject _roomCamera;
        [SerializeField, Tooltip("Optional: This object will be disabled first time the camera is activated")] private GameObject _additionalObject;
        
        public GameObject RoomCamera => _roomCamera;

        public GameObject AdditionalObject => _additionalObject;
    }
}
