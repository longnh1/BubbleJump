using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
	public SpriteRenderer itemSprite;
	public LayerMask interactLayer;

	public Transform trans;

	public BubbleScript bubbleContainer;

	//Inside the bubble
	public virtual void Bubblify() { }
	
	public virtual void Pop() { }

    public virtual void SetParent(BubbleScript parent = null)
    {
        if (parent == null)
        {
            trans.parent = null;
            trans.localScale = Vector3.one;
        }
    }

    //TODO: SHOULD BE USING OBJECT POOLING
    //Push it back to the Pool
    public void DeactiveItem()
    {
        gameObject.SetActive(false);
    }
}
