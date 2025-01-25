using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Sprite itemSprite;
    public LayerMask interactLayer;

    public virtual void StartFollowingPlayer(){

    }

    public virtual void Bubblify(){

    }
    
    public virtual void Pop(){
    	
    }
}
