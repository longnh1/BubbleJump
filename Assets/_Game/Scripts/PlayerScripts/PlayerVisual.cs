using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        if (playerMovement.IsMoving())
        {
            spriteRenderer.flipX = playerMovement.GetPlayerDirection() < 0;
        }
    }


}
