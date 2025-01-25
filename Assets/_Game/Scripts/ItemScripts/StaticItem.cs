using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticItem : ItemBase
{
	public void OnCollisionEnter2D(Collision2D oth){
		GameObject other = (oth.collider==GetComponent<Collider2D>())?oth.otherCollider.gameObject:oth.collider.gameObject;
		if (other.CompareTag(Constant.BUBBLE_TAG)){
			Bubblify();
			other.gameObject.GetComponent<BubbleScript>().DeactiveBubble();
		}else if(other.CompareTag(Constant.PLAYER_TAG)){
			//collect
		}
	}
}
