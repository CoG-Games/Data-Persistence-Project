using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float defaultMovementSpeed = 7f;

    [Header("Movement Range Fields")]
    [SerializeField] private float xRange = 6f;
    [SerializeField] private float yRange = 3f;
    [SerializeField] private float yOffset = -3f;

    [Header("Event Fields")]
    [SerializeField] private VoidEventSO bossDefeated;
    [SerializeField] private VoidEventSO levelVictory;

    private bool isBossDefeated;
    private float movementSpeed;
    private float bossDefeatedFlyAwayDelayTimer;
    void Start()
    {
        isBossDefeated = false;
        movementSpeed = defaultMovementSpeed;
        bossDefeatedFlyAwayDelayTimer = 1f;
    }

    private void OnEnable()
    {
        bossDefeated.OnEventRaised += BeginOutroAnimation;
    }
    
    private void OnDisable()
    {
        bossDefeated.OnEventRaised -= BeginOutroAnimation;
    }

    private void BeginOutroAnimation()
    {
        isBossDefeated = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector;
        if(isBossDefeated)
        {
            if(bossDefeatedFlyAwayDelayTimer>0)
            {
                movementVector = Vector3.zero;
                bossDefeatedFlyAwayDelayTimer -= Time.deltaTime;
            }
            else
            {
                movementVector = movementSpeed * Time.deltaTime * Vector3.up;
                movementSpeed = Mathf.Lerp(movementSpeed, 3f * defaultMovementSpeed, Time.deltaTime);
            }
        }
        else
        {
            Vector2 inputVector = gameInput.GetMovementVector();

            movementVector = new Vector3(inputVector.x, inputVector.y, 0f) * movementSpeed * Time.deltaTime;
            if (!CanMove(movementVector))
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
        }
        
        transform.position += movementVector;
        if(transform.position.y > 7f)
        {
            levelVictory.RaiseEvent();
        }
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
