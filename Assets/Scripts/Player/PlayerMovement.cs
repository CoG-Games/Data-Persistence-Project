using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float movementSpeed = 5f;

    [Header("Movement Range Fields")]
    [SerializeField] private float xRange = 6f;
    [SerializeField] private float yRange = 3f;
    [SerializeField] private float yOffset = -3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 movementVector = new Vector3(inputVector.x, inputVector.y, 0f) * movementSpeed * Time.deltaTime;
        if(!CanMove(movementVector))
        {
            Vector3 modifiedMovementVector = new Vector3(movementVector.x, 0f, 0f);
            if (CanMove(modifiedMovementVector))
            {
                movementVector = modifiedMovementVector;
            }
            else
            {
                modifiedMovementVector = new Vector3(0f, movementVector.y, 0f);
                if (CanMove(modifiedMovementVector))
                {
                    movementVector = modifiedMovementVector;
                }
                else
                {
                    movementVector = Vector3.zero;
                }
            }
        }
        
        transform.position += movementVector;
    }

    private bool CanMove(Vector3 movementVector)
    {
        Vector3 possiblePosition = transform.position + movementVector;

        if (Math.Abs(possiblePosition.x) < xRange && Math.Abs(possiblePosition.y - yOffset) < yRange)
            return true;
        else
            return false;
    }
}
