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

    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private AudioSource walkSource;

    public bool IsExploded { get; set; }
    public bool IsMoving { get; private set; }
        

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

        float inputX = Input.GetAxis("Horizontal");

        if (IsExploded)
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0) {
                //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
                Move(inputX);
            }
        } 
        else
        {
            //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            Move(inputX);
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
                jumpSource.Play();
            }
        }

    }

    #endregion

    #region Public Methods
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

    private void Move(float input)
    {
        if (input != 0) IsMoving = true;
        else IsMoving = false;
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(input * moveSpeed, rb.velocity.y), 10 * Time.deltaTime);
        if ((rb.velocity.x>0.1 || rb.velocity.x<-0.1)&&IsGrounded()){
            walkSource.loop=true;
            if (!walkSource.isPlaying){
                walkSource.Play();
            }
        }else{
            walkSource.loop=false;
            walkSource.Stop();
        }
    }
    #endregion

}
