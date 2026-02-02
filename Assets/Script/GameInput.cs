using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    private PlayerInputActions inputActions;


    //.............................................................
    private bool isRunning = false; 
    public bool IsRunning => isRunning;
    //.............................................................


    private void Awake() 
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += Interact_performed;

        //.............................................................
        inputActions.Player.Sprint.performed += context => isRunning = true;
        inputActions.Player.Sprint.canceled += context => isRunning = false;
        //.............................................................
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        //GetKey - удержание клавиши
        //GetKeyDown - отпускание клавиши

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
