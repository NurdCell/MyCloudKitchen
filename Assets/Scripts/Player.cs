using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.5f;

    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking = false;
    private Vector3 lastMoveDir;
    public bool IsWalking
    {
        get 
        {
            return isWalking;
        }
        set
        {
            isWalking = value;
        }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {
        Vector2 inputVector = gameInput.MovementInputNormalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero) 
        {
            lastMoveDir = moveDir;
        }
        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHitInfo, interactionDistance, countersLayerMask))
        {
            if (raycastHitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {

        HandleMovement();
        //HandleInteractions();
    }

    private void HandleInteractions()
    {
      Vector2 inputVector = gameInput.MovementInputNormalized;
      Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
      float interactionDistance = 2f;
      if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHitInfo, interactionDistance, countersLayerMask)) 
      {
            if (raycastHitInfo.transform.TryGetComponent(out ClearCounter clearCounter)) 
            {
                clearCounter.Interact();
            }
      }
    }

    private void HandleMovement() 
    {
        Vector2 inputVector = gameInput.MovementInputNormalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // if can't move check for a specific direction
        if (!canMove)
        {
            // check for x-axis only
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            // if can move along x only
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // if can't move along X check for movement along Z-axis only
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    // we can't move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero ? true : false;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
}
