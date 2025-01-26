using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
	public SpriteRenderer itemSprite;
	public LayerMask interactLayer;

	public Transform trans;


	//Inside the bubble
	public virtual void Bubblify() { }
	
	public virtual void Pop() { }
	
	
}
