using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticItem : ItemBase
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Collider2D col;

	public void Start()
	{
		transform.parent = null;
	}
	
	public void OnCollisionEnter2D(Collision2D oth){
		GameObject other = (oth.collider == col) ?
			oth.otherCollider.gameObject : oth.collider.gameObject;

		if (other.CompareTag(Constant.BUBBLE_TAG))
		{
			Bubblify();
			transform.parent = other.gameObject.transform;
		}
		else if(other.CompareTag(Constant.PLAYER_TAG))
		{
			//collect
		}
	}

	public void Update()
	{
		if (transform.parent != null)
		{
			transform.localPosition /= 2;
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
        trans.parent = null;
		trans.localScale = Vector3.one;
    }

	public void SetParent(Transform parent = null)
	{
		if (parent == null)
		{

		}
	}
}
