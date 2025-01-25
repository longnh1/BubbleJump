using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Sprite itemSprite;
    public LayerMask interactLayer;

    public virtual void Interact()
    {

    }

    public virtual void Collected()
    {

    }
}
