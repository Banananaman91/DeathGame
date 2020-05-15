using UnityEngine;

namespace MovementNEW
{
    public class RoomNumbers : MonoBehaviour
    {
        [SerializeField] private int _roomToCamera;
        public int RoomToCamera => _roomToCamera;
    }
}
