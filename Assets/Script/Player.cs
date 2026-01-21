using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameInput gameInput;


    [SerializeField]
    private float speed = 10f;


    //.............................................................
    [SerializeField]
    private float runSpeed = 1.6f;
    //.............................................................



    [SerializeField]
    private float rotationSpeed = 10f;

    private Vector3 lastInteractionDir;

    public bool IsWalking { get; private set; }


    //.............................................................
    public bool IsRunning { get; private set; }
    //.............................................................


    private void Update()
    {
        HandelMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) 
        {
            lastInteractionDir = moveDir;
        }


        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractionDir,
            out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
            else
            {
                Debug.Log(raycastHit.transform);
            }
        }
        

    }

    private void HandelMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        IsRunning = gameInput.IsRunning && moveDir != Vector3.zero;

        //.............................................................
        float currentSpeed = speed; // текущая скорость
        if (IsRunning)
        {
            currentSpeed = speed * runSpeed; //бег
        }
        //.............................................................


        float playerRadius = 0.7f;
        float playerHeught = 2f;

        float moveDistance = currentSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * playerHeught, playerRadius,
            moveDir, moveDistance);

        IsWalking = moveDir != Vector3.zero;

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = !Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * playerHeught, playerRadius,
            moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeught, playerRadius,
                moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }

            }

        }


        if (canMove)
        {
            transform.position += currentSpeed * moveDir * Time.deltaTime;
        }

        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

        }
    }
}
