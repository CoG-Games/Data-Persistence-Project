using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private VoidEventSO bossDefeated;

    private PlayerControls playerControls;
    
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    private void OnEnable()
    {
        bossDefeated.OnEventRaised += DisableControls;
    }

    private void OnDisable()
    {
        bossDefeated.OnEventRaised -= DisableControls;
    }

    private void DisableControls()
    {
        playerControls.Player.Disable();
    }

    public Vector2 GetMovementVector()
    { 
        return playerControls.Player.Move.ReadValue<Vector2>();
    }
}
