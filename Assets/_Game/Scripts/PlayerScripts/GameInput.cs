using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : Singleton<GameInput>
{
    #region Member Variables

    private PlayerInputActions playerInputActions;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    #endregion

    #region Public Methods
    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool GetJumpKey()
    {
        float isJump = playerInputActions.Player.Jump.ReadValue<float>();
        return isJump > 0f;
    }

    #endregion
}
