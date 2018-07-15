using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    //************************************************************************************
    //  Game Controller 
    //      by: Marc Edison Estaca
    //  This is present in every game. All the game logic is placed here.
    //************************************************************************************
    private bool _mouseState = false;

    private void Start()
    {
        // locks the mouse
        LockCUrsor(_mouseState);
    }

    private void Update()
    {
        //************************************************************************************
        //  Locks/Unlocks mouse
        //************************************************************************************
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            // if the mouse state is true set it to false vice versa
            changeState();

            // apply new mouse state
            LockCUrsor(getMouseState());
        }
    }

    #region PublicFunctions

    /// <summary>
    /// Returns the current mouse state value.
    /// </summary>
    /// <returns>boolean</returns>
    public bool getMouseState()
    {
        return _mouseState;
    }

    /// <summary>
    /// Public function that negates the current mouse state
    /// </summary>
    public void changeState()
    {
        _mouseState = !_mouseState;
    }

    /// <summary>
    /// Shows/Hides the mouse and locks it in the center of the screen
    /// </summary>
    /// <param name="enabled">true to show and lock mouse, false if the opposite</param>
    public void LockCUrsor(bool enabled)
    {

        //  is mouse visible
        Cursor.visible = enabled;

        //  locks the mouse in the center of the screen
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;

    }

    #endregion
}


