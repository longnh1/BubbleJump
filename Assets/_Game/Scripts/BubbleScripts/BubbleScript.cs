using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShockWave {
	public Vector3 origin;
	public float size;
}

public class BubbleScript : MonoBehaviour
{
	[SerializeField] private float size = 0.1f;
	[SerializeField] private float speed=1;
	

	[SerializeField] private LayerMask collidedLayer;

	public float targetSize = 1;
	public Vector3 targetPosition;

	private Collider2D selfCollider;

	public ItemBase TrappedItem { get; private set; }

	void Start(){
		//retrieve self collider
		selfCollider=GetComponent<Collider2D>();
        transform.localScale = Vector3.one * size;
    }

    private void Update()
	{
        if (!LevelManager.Instance.IsLevelStart) return;

        //move the bubble with an ease-out effect (janky)
        Vector3 diff = targetPosition - transform.position;
		float coef = speed * Time.deltaTime;
		//do not go farther than the target pos
		coef = coef > 1 ? 1 : coef;
		transform.position = transform.position + (diff * coef);

		if (size < targetSize)
		{
			size += Time.deltaTime * 3;
			size = size > targetSize ? targetSize : size;
		}

		transform.localScale = Vector3.one * size;
	}

	void FixedUpdate(){

        if (!LevelManager.Instance.IsLevelStart) return;

        // checks collisions
        RaycastHit2D[] cols = Physics2D.CircleCastAll(transform.position, size/2, Vector2.zero, 0, collidedLayer);
		foreach (RaycastHit2D col in cols){
			if (col.collider != selfCollider){
				GameObject oth = col.collider.gameObject;

				// if we collide with another bubble, fuse parameters and deactivate the other bubble
				if (oth.CompareTag(Constant.BUBBLE_TAG))
				{
					BubbleScript othBub = oth.GetComponent<BubbleScript>();
					// position average
					transform.position = (transform.position+oth.transform.position)/2;
					//taget position average
					targetPosition = (targetPosition+othBub.targetPosition)/2;
					//size merging
					targetSize = targetSize + othBub.targetSize;
					size = size>othBub.size? size : othBub.size;
					othBub.DeactiveBubble();

					if (HasItemInside())
					{
						Pop();
					}
				}
				else
				{
					if (oth.CompareTag(Constant.GROUND_TAG))
					{
						Pop();
					}
				}
			}
		}

	}

	public void Pop(){
		PopItemInside();
        ShockWave sw;
		sw.origin=transform.position;
		sw.size=size;
		transform.parent.gameObject.BroadcastMessage(nameof(ApplyShockWave), sw );
		DeactiveBubble();
	}
	
	public void ApplyShockWave(ShockWave sw){
		float dist = Vector3.Distance(targetPosition,sw.origin);
		Vector3 dir = (targetPosition - sw.origin);
		dir.Normalize();
		targetPosition += dir * (1.0f/dist) * (sw.size*sw.size);
	}

	public void Explode()
	{
		Collider2D[] inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, size);

		foreach (var item in inExplosionRadius)
		{
			Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				PlayerMovement playerMovement = rb.transform.GetComponent<PlayerMovement>();
				if (playerMovement != null)
				{
					playerMovement.IsExploded = true;
				}
				rb.velocity = Vector2.zero;
				rb.angularVelocity = 0;

				Vector2 distanceVector = item.transform.position - transform.position;
				if (distanceVector.magnitude > 0)
				{
					float explosionForce = (size * size * 200) / distanceVector.magnitude;

					//Debug.Log(distanceVector.normalized);
					rb.AddForce(distanceVector.normalized * explosionForce);
				}
			}
		}

		Pop();
	}

	public void DeactiveBubble()
	{
		PopItemInside();
        size = 0.1f;
		targetSize = 1.0f;
		transform.localScale = Vector3.one * size;
		gameObject.SetActive(false);
	}

	public void SetItem(ItemBase item = null)
	{
		TrappedItem = item;
	}

	public bool HasItemInside()
	{
		return TrappedItem != null;
	}

	private void PopItemInside()
	{
        if (HasItemInside())
        {
            if (TrappedItem is StaticItem)
            {
                StaticItem si = (StaticItem)TrappedItem;
                si.Pop();
            }
            else //Special Item
            {

            }
        }
		SetItem(null);
    }
}
