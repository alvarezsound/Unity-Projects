using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public AudioSource audioController;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound()
    {
        AudioController.instance.Play(hoverSound);
    }
    public void ClickSound()
    {
        AudioController.instance.Play(clickSound);
    }
}
