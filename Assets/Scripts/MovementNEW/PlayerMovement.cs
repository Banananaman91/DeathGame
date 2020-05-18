using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace MovementNEW
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _playerInput;
        private Quaternion _playerRotation;
        [SerializeField] private Rigidbody _playerRb;
        [SerializeField] private CameraMovement _cameraScript;
    
        // Update is called once per frame
        void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            _playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (_playerInput == Vector3.zero) return;
            _playerInput = _playerInput.normalized;
            _playerRb.velocity = (_cameraScript.CameraF * _playerInput.z + _cameraScript.CameraR * _playerInput.x) * speed;
            _playerRotation = Quaternion.LookRotation(_playerRb.velocity);
            _playerRb.rotation = _playerRotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            var roomNumber = other.gameObject.GetComponent<RoomCameras>();
            if (roomNumber)
            {
                _cameraScript.ChangeRoom(roomNumber.RoomCamera);
            }
        }
    }
}
