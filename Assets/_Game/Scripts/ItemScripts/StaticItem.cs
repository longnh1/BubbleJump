using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticItem : ItemBase
{
	public void Start(){
		transform.parent=null;
	}
	
	public void OnCollisionEnter2D(Collision2D oth){
		GameObject other = (oth.collider==GetComponent<Collider2D>())?oth.otherCollider.gameObject:oth.collider.gameObject;
		if (other.CompareTag(Constant.BUBBLE_TAG)){
			Bubblify();
			transform.parent = other.gameObject.transform;
		}
		else if(other.CompareTag(Constant.PLAYER_TAG)){
			//collect
		}
	}
	
	public void Update(){
		if (transform.parent!=null){
			transform.localPosition/=2;
		}
	}
}
