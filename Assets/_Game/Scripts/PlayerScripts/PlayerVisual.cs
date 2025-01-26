using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private BubbleSpawner bubbleSpawner;


    private const string IS_MOVING_ANIMATION = "IsMoving";
    private const string IS_BANGING_ANIMATION = "IsBanging";

    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStart) return;

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(IS_BANGING_ANIMATION, true);
        }

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

    public void SpawnBubble()
    {
        bubbleSpawner.SpawnBubble();
    }

    public void EndBubbleAnim()
    {
        animator.SetBool(IS_BANGING_ANIMATION, false);
    }
}
