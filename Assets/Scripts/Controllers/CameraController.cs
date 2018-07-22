using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//************************************************************************************
//
//  Basic Camera Controller by Marc Edison Estaca
//
//  A very simple camera controller that follows the target object.
//  The camera is always placed at the back of the target object.
//
//************************************************************************************
public class CameraController : MonoBehaviour
{

    #region PublicVariables
    //************************************************************************************
    //  Public Variables
    //************************************************************************************
    [Tooltip("Target Object")]
    public GameObject targetObject;

    [Tooltip("Distance between the camera and the target")]
    public Vector3 cameraOffset = new Vector3(0,6,0);

    [Tooltip("Look up and down speed")]
    public float mouseYSensitivity = 10f;

    [Tooltip("Camera zooming speed")]
    public float zoomSensitivity = 10f;

    [Tooltip("Current zoom distance"), Range(Constants.CameraZoom.MIN,Constants.CameraZoom.MAX)]
    public float zoomDistance = 10f;
    #endregion

    #region PrivateVariables
    //************************************************************************************
    //  Private Variables
    //************************************************************************************

    //  local variable for handling mouse state which shows/hides and locks/unlocks the mouse
    private bool _mouseState;

    //  local variable for handling camera Y offset
    private float _cameraElevation;

    //  camera smoothing factor
    private float _cameraSmoothingFactor = 10f;

    //  local variable for handling camera offset
    private Vector3 _cameraAdjustment;

    // local variable for handling the instance of the game controller
    private GameController _gameController;
    #endregion

    private void Start()
    {

        // get the game controller
        _gameController = FindObjectOfType<GameController>();

        // get mouse state
        _mouseState = _gameController.GetMouseState();

    }

    private void Update()
    {

        //  checkm mouse state
        if (!_gameController.GetMouseState())
        { 
            //  calculates camera elevation based on the mouse Y
            _cameraElevation += Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;
            _cameraElevation = Mathf.Clamp(_cameraElevation, Constants.CameraElevation.MIN, Constants.CameraElevation.MAX); 

            //  calculates camera offset
            _cameraAdjustment.Set(0, _cameraElevation, 0);

            //  calculates camera zoom distance
            zoomDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
            zoomDistance = Mathf.Clamp(zoomDistance, Constants.CameraZoom.MIN, Constants.CameraZoom.MAX);
        }

    }

    private void FixedUpdate()
    {

        //  applies camera position and elevation
        Vector3 _targetPosition = (targetObject.transform.position + (targetObject.transform.forward * -zoomDistance)) + cameraOffset;

        //  smooths the camera
        Vector3 _smoothPosition = Vector3.Lerp(transform.position, _targetPosition, _cameraSmoothingFactor * Time.deltaTime);

        //  moves the camera to the smooth location
        transform.position = _smoothPosition;

        //  points the camera to the target object
        transform.LookAt(targetObject.transform.position + _cameraAdjustment);

    }

}
