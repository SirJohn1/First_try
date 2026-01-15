using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake() 
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }




    public Vector2 GetMovementVectorMormalize()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        //GetKey - удержание клавиши
        //GetKeyDown - отпускание клавиши

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
