using InventoryScripts;
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
            if (Input.touchCount > 0)
            {
                //Store the first touch
                Touch myTouch = Input.touches[0];
                //Check if the phase of that touch equals Began
                if (myTouch.phase == TouchPhase.Began)
                {
                    //if so, set touchOrigin to the position of that touch
                    _touchOrigin = myTouch.position;
                }
                //If the touch phase is not began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero
                else if (myTouch.phase == TouchPhase.Moved && _touchOrigin.x >= 0)
                {
                    //set touchend to equal the position of this touch
                    Vector2 touchEnd = myTouch.position;
                    
                    //Calculate the difference between the beginning and end of the touch on the x axis
                    horizontal = touchEnd.x - _touchOrigin.x;
                    
                    //Calculate the difference between the beginning and end of the touch on the y axis
                    vertical = touchEnd.y - _touchOrigin.y;

                    _playerInput = new Vector3(horizontal, 0, vertical);
                }
                else if (myTouch.phase == TouchPhase.Ended) _playerInput = Vector3.zero;
            }
            
            else _playerInput = Vector3.zero;
            
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
                _cameraScript.ChangeRoom(roomNumber.RoomCamera);
            }
        }
    }
}
