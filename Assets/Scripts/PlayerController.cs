using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//************************************************************************************
//
//  Basic Player Controller by Marc Edison Estaca
//
//  A very simple player controller that allows scripted object to move and jump
//  The camera is always placed at the back of the target object.
//
//************************************************************************************
public class PlayerController : MonoBehaviour
{

    #region PublicVariables
    //************************************************************************************
    //  Public Variables
    //************************************************************************************
    [Tooltip("Rotation Speed"),Range(Constants.MouseSensitivityX.MIN,Constants.MouseSensitivityX.MAX)]
    public float mouseSensitivityX = 50f;
    [Tooltip("Jump Height")]
    public float jumpHeight = 8f;
    [Tooltip("Player Speed")]
    public float playerSpeed = 10f;
    #endregion

    #region PrivateVariables
    //************************************************************************************
    //  Private Variables
    //************************************************************************************

    // local variable to check if the player is jumping
    private bool _isJumping;

    // local variable for handling the rigid body component
    private Rigidbody _rigidBody;

    // local variable for handling the game controller object
    private GameController _gameController;
    #endregion  

    private void Start()
    {

        // get the game controller
        _gameController = FindObjectOfType<GameController>();

        // get the rigidbody
        _rigidBody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //  check mouse state
        if (!_gameController.getMouseState())
        {

            // apply rotation to player
            RotatePlayer();

            //  checks space key, checks if grounded, checks if not jumping (this one probably needs revision)
            if (Input.GetKey(KeyCode.Space) && isGrounded() && !_isJumping)
            {
                // flags jumping and not a way to determine if the object is jumping
                _isJumping = true;
            }
        }

    }

    private void FixedUpdate()
    {

        // allows player to move
        MovePlayer();

        // allows player to jump
        JumpPlayer();

    }

    #region PrivateFunctions

    //  rotate the player object based on the accumulated mouse x offset
    private void RotatePlayer()
    {
        // rotation sensitivity is handled by the mouseSensitivityX variable
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime, 0));
    }

    //  moves the player object 
    private void MovePlayer()
    {
        // gets movement input
        float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveVertical = Input.GetAxis("Vertical");

        //  sets new direction
        Vector3 _direction = new Vector3(_moveHorizontal, 0, _moveVertical);

        //  move player object
        transform.Translate(_direction * playerSpeed * Time.deltaTime);
    }

    // simple jump script (needs revision)
    private void JumpPlayer()
    {

        // checkes if the player is jumping
        if(_isJumping)
        {

            // applies velocity
            _rigidBody.velocity = transform.up * jumpHeight;

            // inhibit application of more velocity
            _isJumping = false;

        }

        
    }
    #endregion

    #region PublicFunctions
    /// <summary>
    ///  checks if the player object's transform is grounded
    /// </summary>
    /// <returns>boolean</returns>
    public bool isGrounded()
    {
        RaycastHit _ray;
        Physics.Raycast(transform.position, -transform.up, out _ray);

        return _ray.distance <= 1 && _ray.collider != null;
    }

    #endregion
}

