using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource shootSound = null;

        public enum AudioType
        {
            Shoot
        }

        public void Play(AudioType audioType, bool playState)
        {
            AudioSource audioSource = null;
            switch (audioType)
            {
                case AudioType.Shoot:
                    audioSource = shootSound;
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
}

