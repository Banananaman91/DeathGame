using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _lookPoint;
    private Vector3 _cameraF;
    private Vector3 _cameraR;
    private Transform cameraPivot => currentCamera.transform.parent;
    private bool _startedRotation;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject currentCamera;
    [SerializeField] private GameObject[] listOfCameras;
    public Vector3 CameraF => _cameraF;
    public Vector3 CameraR => _cameraR;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < listOfCameras.Length; i++)
        {
            if(listOfCameras[i] == currentCamera) continue;
            listOfCameras[i].SetActive(false);
        }
        _lookPoint = cameraPivot.position;
        UpdateCamera();
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
        Vector3 goalEuler = new Vector3(0, cameraPivot.transform.eulerAngles.y + angle, 0);
        //cameraPivot.eulerAngles = goalEuler; //Line of code to bring back snappy camera rotation
        _startedRotation = true;
        StartCoroutine(SmoothRotate(goalEuler));
    }
    
    private void UpdateCamera()
    {
        Transform camT = currentCamera.transform;
        camT.LookAt(_lookPoint);
        _cameraF = new Vector3(camT.forward.x, 0f, camT.forward.z).normalized;
        _cameraR = new Vector3(camT.right.x, 0f, camT.right.z).normalized;
    }

    public void ChangeRoom(int room)
    {
        var angle = cameraPivot.rotation.eulerAngles;
        currentCamera.SetActive(false);
        currentCamera = listOfCameras[room];
        currentCamera.SetActive(true);
        _lookPoint = currentCamera.transform.parent.position;
        if (cameraPivot.rotation.eulerAngles != angle)
        {
            cameraPivot.eulerAngles = angle;
        }
        UpdateCamera();
    }

    IEnumerator SmoothRotate(Vector3 goalEuler)
    {
        do
        {
            float angle = Mathf.LerpAngle(cameraPivot.eulerAngles.y, goalEuler.y, rotateSpeed * Time.deltaTime);
            cameraPivot.eulerAngles = new Vector3(0,angle,0);
            var distance = Vector3.Distance(cameraPivot.eulerAngles, goalEuler);
            if (distance < 0.1f || distance > 360 && distance < 361) //Needs redoing potentially
            {
                _startedRotation = false;
                var euler = new Vector3(0, Mathf.Round(cameraPivot.eulerAngles.y),0);
                cameraPivot.eulerAngles = euler;
            }
            UpdateCamera();
            yield return null;
        } while (_startedRotation);
    }
}