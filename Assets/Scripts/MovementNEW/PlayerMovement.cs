using InventoryScripts;
using SimpleInputNamespace;
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
        private Vector2 _touchOrigin = -Vector2.one;
        [SerializeField] private Rigidbody _playerRb;
        [SerializeField] private CameraMovement _cameraScript;
        [SerializeField] private JournalInventoryScript _journalInventory;
        [SerializeField] private Joystick _joystick;

        public JournalInventoryScript JournalInventory => _journalInventory;

        public Vector3 RayDir => transform.forward;
    
        // Update is called once per frame
        void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float horizontal = 0;
            float vertical = 0;
#if UNITY_STANDALONE
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            _playerInput = new Vector3(horizontal, 0, vertical);
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
            //Check if input has registered more than zero touches
            _playerInput = new Vector3(_joystick.xAxis.value, 0, _joystick.yAxis.value);

#endif
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
                if (roomNumber.AdditionalObject) roomNumber.AdditionalObject.SetActive(false);
                _cameraScript.ChangeRoom(roomNumber.RoomCamera);
            }
        }
    }
}
