using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
	public float size = 1;
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
				if (oth.CompareTag("bubble")){
					BubbleScript othBub = oth.GetComponent<BubbleScript>();
					transform.position=(transform.position+oth.transform.position)/2;
					targetPosition=(targetPosition+othBub.targetPosition)/2;
					size = size+othBub.size;
					othBub.size=1;
					oth.SetActive(false);
				}
			}
		}
		transform.localScale=new Vector3(size,size,size);
	}
}
