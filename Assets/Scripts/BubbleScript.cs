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
		List<RaycastHit2D> collisions = new List<RaycastHit2D>();
		int col = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), size/2, new Vector2(0,0), new ContactFilter2D().NoFilter(), collisions);
		if (col>1){
			Debug.Log(col);
			for (int i=0; i<col; i++){
				if ((collisions[i]!=selfCollider)){
					Debug.Log("blop");
				}
			}
		}
	}
}
