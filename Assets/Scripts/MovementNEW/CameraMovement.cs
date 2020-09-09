using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private List<GameObject> _listOfCameras;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private GameObject _currentCamera;
        public Vector3 CameraF => _cameraF;
        public Vector3 CameraR => _cameraR;
        private Transform CameraPivot => _currentCamera.transform.parent;
        private Transform CameraTransform => _currentCamera.transform;
        // Start is called before the first frame update
        void Start()
        {
            if (_listOfCameras.Count == 0) //For use during development, cameras will be pre-defined later
            {
                var allCameras = FindObjectsOfType<Camera>();
                foreach (var cam in allCameras)
                {
                    _listOfCameras.Add(cam.GetComponent<Transform>().gameObject);
                }
            }
            foreach (var cam in _listOfCameras)
            {
                if(cam == _currentCamera) continue;
                cam.SetActive(false);
            }
            _lookPoint = CameraPivot.position;
            UpdateViewAndMovement();
        }
#if UNITY_STANDALONE
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
        
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        public void RotateLeft(int angle)
        {
            if (_startedRotation) return;
            RotateCamera(angle);
        }

        public void RotateRight(int angle)
        {
            if (_startedRotation) return;
            RotateCamera(angle);
        }
#endif
    
        private void RotateCamera(int angle)
        {
            _goalRotateEuler = new Vector3(0, CameraPivot.transform.eulerAngles.y + angle, 0);
            _startedRotation = true;
            StartCoroutine(SmoothRotate(_goalRotateEuler));
        }
        
        private void UpdateViewAndMovement()
        {
            //UpdateCameraView();
            UpdateMovementDirection();
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
        
        public void ChangeRoom(GameObject roomCamera)
        {
            var angle = CameraPivot.rotation.eulerAngles;
            _currentCamera.SetActive(false);
            _currentCamera = roomCamera;
            _currentCamera.SetActive(true);
            _lookPoint = _currentCamera.transform.parent.position;
            if (CameraPivot.rotation.eulerAngles != angle)
            {
                CameraPivot.eulerAngles = angle;
            }
            UpdateViewAndMovement();
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
                //UpdateCameraView();
                yield return null;
            } while (_startedRotation);
            UpdateMovementDirection();
        }
    }
}