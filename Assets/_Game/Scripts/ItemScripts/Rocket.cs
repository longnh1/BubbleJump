using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constant.PLAYER_TAG))
        {
            EventManager.SendSimpleEvent(Events.CHECK_WIN_CONDITION);
            Debug.Log("PLAYER ENTER THE ROCKET");
        }
    }
}
