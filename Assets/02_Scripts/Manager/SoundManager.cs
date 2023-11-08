using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //GameManager.gm.soundManager.Play(SoundManager.AudioType.MetalDoor, true);

    [SerializeField] AudioSource ghostSound = null;
    [SerializeField] AudioSource metalDoorSound = null;
    [SerializeField] AudioSource stoneSound = null;
    [SerializeField] AudioSource cubeSound = null;
    [SerializeField] AudioSource clickSound = null;
    [SerializeField] AudioSource windSound = null;
    [SerializeField] AudioSource stonefallSound = null;


    public enum AudioType
    {
        Ghost, MetalDoor, Stone, Cube, Click, Wind, Stonefall
    }


    public void Play(AudioType audioType, bool playState)
    {
        AudioSource audioSource = null;
        switch (audioType)
        {
            case AudioType.Ghost:
                audioSource = ghostSound;
                break;
            case AudioType.MetalDoor:
                audioSource = metalDoorSound;
                break;
            case AudioType.Stone:
                audioSource = stoneSound;
                break;
            case AudioType.Cube:
                audioSource = cubeSound;
                break;
            case AudioType.Click:
                audioSource = clickSound;
                break;
            case AudioType.Wind:
                audioSource = clickSound;
                break;
            case AudioType.Stonefall:
                audioSource = clickSound;
                break;
        }
        if (audioSource != null)
        {
            if (playState)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }


}
