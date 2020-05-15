using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool _startedRotation;
    private Vector3 _lookPoint;
    private Vector3 _cameraF;
    private Vector3 _cameraR;
    private Vector3 _goalRotateEuler;
    private Quaternion _goalRotateQuat;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject currentCamera;
    [SerializeField] private GameObject[] listOfCameras;
    public Vector3 CameraF => _cameraF;
    public Vector3 CameraR => _cameraR;
    private Transform CameraPivot => currentCamera.transform.parent;
    private Transform CameraTransform => currentCamera.transform;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cam in listOfCameras)
        {
            if(cam == currentCamera) continue;
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
        currentCamera.SetActive(false);
        currentCamera = listOfCameras[room];
        currentCamera.SetActive(true);
        _lookPoint = currentCamera.transform.parent.position;
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
            CameraPivot.rotation = Quaternion.RotateTowards(CameraPivot.rotation, _goalRotateQuat, rotateSpeed);
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