using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private PlayerCollection playerCollection;

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
        if (!LevelManager.Instance.IsLevelStart) return;

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

    #region Public Methods

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }

    public float GetPlayerDirection()
    {
        return rb.velocity.x;
    }

    #endregion

    #region Private Methods

    private void IsTouchedBubble()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.CapsuleCastAll(transform.position + Vector3.up * -0.13f, new Vector2(.5f, .75f),
            CapsuleDirection2D.Vertical, 0, Vector2.zero, 0.0f, bubbleLayer);
        
        if (raycastHit2Ds != null)
        {
            foreach (RaycastHit2D raycastHit2D in raycastHit2Ds) {
                if (raycastHit2D.transform.GetComponent<BubbleScript>())
                {
                    BubbleScript bb = raycastHit2D.transform.GetComponent<BubbleScript>();
                    if (bb != null)
                    {
                        if (bb.HasItemInside())
                        {
                            ItemBase item = bb.TrappedItem;

                            //Collect => Add to player collection
                            playerCollection.CollectionItem(item);

                            //Deactive bubble
                            //Reset item
                            item.SetParent();
                            item.DeactiveItem();

                            bb.SetItem();
                            bb.DeactiveBubble();
                        }
                        else
                        {
                            //Bubble exploded"
                            bb.Explode();
                            //Touched bubble
                            IsExploded = true;
                            moveTimer = MOVE_DELAY;
                        }
                    }
                }
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
