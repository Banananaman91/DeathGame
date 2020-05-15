using System.Collections;
using UnityEngine;

namespace MovementNEW
{
    public class CameraMovement : MonoBehaviour
    {
        private bool _startedRotation;
        private Vector3 _lookPoint;
        private Vector3 _cameraF;
        private Vector3 _cameraR;
        private Vector3 _goalRotateEuler;
        private Quaternion _goalRotateQuat;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private GameObject _currentCamera;
        [SerializeField] private GameObject[] _listOfCameras;
        public Vector3 CameraF => _cameraF;
        public Vector3 CameraR => _cameraR;
        private Transform CameraPivot => _currentCamera.transform.parent;
        private Transform CameraTransform => _currentCamera.transform;
        // Start is called before the first frame update
        void Start()
        {
            foreach (var cam in _listOfCameras)
            {
                if(cam == _currentCamera) continue;
                cam.SetActive(false);
            }
            _lookPoint = CameraPivot.position;
            UpdateCameraView();
            UpdateMovementDirection();
        }

        // Update is called once per frame
        void Update()
        {
            if (_startedRotation) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                RotateCamera(-90);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateCamera(90);
            }
        }
    
        private void RotateCamera(int angle)
        {
            _goalRotateEuler = new Vector3(0, CameraPivot.transform.eulerAngles.y + angle, 0);
            _startedRotation = true;
            StartCoroutine(SmoothRotate(_goalRotateEuler));
        }
    
        private void UpdateCameraView()
        {
            CameraTransform.LookAt(_lookPoint);
        }
    
        private void UpdateMovementDirection()
        {
            _cameraF = new Vector3(CameraTransform.forward.x, 0f, CameraTransform.forward.z).normalized;
            _cameraR = new Vector3(CameraTransform.right.x, 0f, CameraTransform.right.z).normalized;
        }

        public void ChangeRoom(int room)
        {
            var angle = CameraPivot.rotation.eulerAngles;
            _currentCamera.SetActive(false);
            _currentCamera = _listOfCameras[room];
            _currentCamera.SetActive(true);
            _lookPoint = _currentCamera.transform.parent.position;
            if (CameraPivot.rotation.eulerAngles != angle)
            {
                CameraPivot.eulerAngles = angle;
            }
            UpdateCameraView();
        }

        IEnumerator SmoothRotate(Vector3 goalEuler)
        {
            do
            {
                _goalRotateQuat = Quaternion.Euler(goalEuler);
                CameraPivot.rotation = Quaternion.RotateTowards(CameraPivot.rotation, _goalRotateQuat, _rotateSpeed);
                if (Quaternion.Angle(CameraPivot.rotation, _goalRotateQuat) < 0.1f)
                {
                    _startedRotation = false;
                }
                UpdateCameraView();
                yield return null;
            } while (_startedRotation);
            UpdateMovementDirection();
        }
    }
}