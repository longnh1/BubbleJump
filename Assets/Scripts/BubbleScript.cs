using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShockWave{
	public Vector3 origin;
	public float size;
}

public class BubbleScript : MonoBehaviour
{
	public float size = 0.1f;
	public float targetSize = 1;
	public Vector3 targetPosition;
	public float speed=1;
	private Collider2D selfCollider;
	// Start is called before the first frame update
	void Start(){
		//retrieve self collider
		selfCollider=GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void FixedUpdate(){
		//move the bubble with an ease-out effect (janky)
		Vector3 diff=targetPosition-transform.position;
		float coef=speed*Time.deltaTime;
		//do not go farther than the target pos
		coef=coef>1?1:coef;
		transform.position=transform.position+(diff*coef);
		
		// checks collisions
		RaycastHit2D[] cols = Physics2D.CircleCastAll(transform.position, size/2, Vector2.zero);
		foreach (RaycastHit2D col in cols){
			if (col.collider!=selfCollider){
				GameObject oth=col.collider.gameObject;
				Debug.Log(oth.name);
				// if we collide with another bubble, fuse parameters and deactivate the other bubble
				if (oth.CompareTag("bubble")){
					BubbleScript othBub = oth.GetComponent<BubbleScript>();
					// position average
					transform.position=(transform.position+oth.transform.position)/2;
					//taget position average
					targetPosition=(targetPosition+othBub.targetPosition)/2;
					//size merging
					targetSize = targetSize+othBub.targetSize;
					size=size>othBub.size?size:othBub.size;
					othBub.size=0.1f;
					othBub.targetSize=1.0f;
					oth.SetActive(false);
				}
			}
		}
		// increase the scale
		if (size<targetSize){
			size+=Time.deltaTime*3;
			size=size>targetSize?targetSize:size;
		}
		// make the scale visible
		transform.localScale=new Vector3(size,size,size);
	}
	
	public void pop(){
		Debug.Log("Popped!");
		ShockWave sw;
		sw.origin=transform.position;
		sw.size=size;
		transform.parent.gameObject.BroadcastMessage("applyShockWave", sw );
		size=0.1f;
		targetSize=1.0f;
		gameObject.SetActive(false);
	}
	
	public void applyShockWave(ShockWave sw){
		float dist = Vector3.Distance(targetPosition,sw.origin);
		Vector3 dir = (targetPosition-sw.origin);
		dir.Normalize();
		targetPosition += dir*(1.0f/dist)*(sw.size*sw.size);
	}
}
