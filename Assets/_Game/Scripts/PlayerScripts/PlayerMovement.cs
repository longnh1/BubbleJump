using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    # region Inspector Variables

    //[SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D groundedBoxCol;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 3f;

    [SerializeField] private LayerMask collidedLayer;
    [SerializeField] private LayerMask bubbleLayer;

    public bool IsExploded { get; set; }

    #endregion

    #region Member Variables

    private float playerRadius = .5f;
    private float playerHeight = 0.8f;

    private float jumpTimer = 0;
    private float moveTimer = 0;

    private const float JUMP_DELAY = 0.1f;
    private const float MOVE_DELAY = 0.35f;

    #endregion

    #region Unity Methods

    private void Update()
    {
        /*Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Debug.Log(inputVector);*/

        //Vector3 moveDir = new Vector3(inputVector.x, 0, 0);

        //rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);

        IsTouchedBubble();

        if (IsExploded)
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0) {
                //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y), 10* Time.deltaTime);
            }
        } 
        else
        {
            //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y), 10 * Time.deltaTime);
        }



        //Flip player sprite
        //if (IsMoving) playerSprite.flipX = moveDir.x < 0;

        if (IsGrounded()) 
        {
            jumpTimer -= Time.deltaTime;
            IsExploded = false;
            if (jumpTimer <= 0 && GameInput.Instance.GetJumpKey())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTimer = JUMP_DELAY;
            }
        }

    }

    #endregion

    #region Private Methods

    private void IsTouchedBubble()
    {
        RaycastHit2D raycastHit2D = Physics2D.CapsuleCast(transform.position, new Vector2(.5f, 1f),
            CapsuleDirection2D.Vertical, 0, Vector2.zero, 0.0f, bubbleLayer);

        if (raycastHit2D.collider != null)
        {
            //Touched bubble
            IsExploded = true;
            moveTimer = MOVE_DELAY;

            BubbleScript bb = raycastHit2D.transform.GetComponent<BubbleScript>();
            if (bb != null) {
                //Bubble exploded"
                bb.Explode(); 
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(groundedBoxCol.bounds.center, 
            groundedBoxCol.bounds.size, 0, Vector2.down, 0.1f, collidedLayer);
        return raycastHit2D.collider != null;
    }

    #endregion

}
