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


    public enum AudioType
    {
        Ghost, MetalDoor, Stone, Cube
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
