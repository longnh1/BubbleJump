using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource menuSound;

    void Start()
    {
        Button[] buttons = GameObject.FindObjectsOfType<Button>(true); // parameter makes it include inactive UI elements with buttons
        foreach (Button b in buttons){
            b.onClick.AddListener(ButtonSound);
        }
    }
    public void ButtonSound()
    {
        menuSound.Play();
    }
}
