using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    public Vector2 GetMovementVector()
    { 
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

}
