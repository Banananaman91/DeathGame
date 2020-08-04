using UnityEngine;

namespace MovementNEW
{
    public class RoomCameras : MonoBehaviour
    {
        [SerializeField] private GameObject _roomCamera;
        public GameObject RoomCamera => _roomCamera;
    }
}
