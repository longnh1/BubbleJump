using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticItem : ItemBase
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Collider2D col;

	public void OnCollisionEnter2D(Collision2D oth){
		GameObject other = (oth.collider == col) ?
			oth.otherCollider.gameObject : oth.collider.gameObject;

		if (other.CompareTag(Constant.BUBBLE_TAG))
		{
			BubbleScript container = other.GetComponent<BubbleScript>();
			SetParent(container);
			container.SetItem(this);
			Bubblify();
			
		}
		else if(other.CompareTag(Constant.PLAYER_TAG))
		{
			//collect
		}
	}

	public override void Update()
	{
		base.Update();
		if (transform.parent != null)
		{
			if (Vector2.Distance(trans.localPosition, Vector2.zero) > 0.01f)
			{
                transform.localPosition /= 2;
            }
		}
	}

	public override void Bubblify()
    {
        rb.simulated = false;
        col.isTrigger = true;
    }

    public override void Pop()
    {
        rb.simulated = true;
        col.isTrigger = false;
		SetParent();
    }

	public override void SetParent(BubbleScript parent = null)
	{
		if (parent == null)
		{
            trans.parent = null;
            trans.localScale = Vector3.one;
        }
        else
        {
            trans.SetParent(parent.transform);
			bubbleContainer = parent;
        }
    }
}
