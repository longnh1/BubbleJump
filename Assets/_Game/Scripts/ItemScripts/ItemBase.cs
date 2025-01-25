using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
	public Sprite itemSprite;
	public LayerMask interactLayer;
	public bool bubbled;
	public bool followingPlayer;

	/*public virtual void StartFollowingPlayer(){
		followingPlayer=True;
	}*/

	//Inside the bubble
	public virtual void Bubblify(){
		bubbled=true;
		GetComponent<Rigidbody2D>().simulated=false;
		GetComponent<Collider2D>().isTrigger=true;
	}
	
	public virtual void Pop(){
		bubbled=false;
		followingPlayer=false;
		GetComponent<Rigidbody2D>().simulated=true;
		GetComponent<Collider2D>().isTrigger=false;
	}
}
