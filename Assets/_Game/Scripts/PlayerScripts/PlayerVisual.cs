using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerMovement playerMovement;


    private const string IS_MOVING_ANIMATION = "IsMoving";

    private void Update()
    {
        if (playerMovement.IsMoving)
        {
            spriteRenderer.flipX = playerMovement.GetPlayerDirection() < 0;
            animator.SetBool(IS_MOVING_ANIMATION, true);
        }
        else
        {
            animator.SetBool(IS_MOVING_ANIMATION, false);
        }
    }


}
