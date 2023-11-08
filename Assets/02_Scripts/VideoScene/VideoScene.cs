using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace whale
{
    public class VideoScene : MonoBehaviour
    {
        public VideoPlayer videoPlayer;

        private void Start()
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }

        void OnVideoEnd(VideoPlayer vp)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
            MainManager.Instance.loadingManager.LoadSceneAsync("03_GameScene");
        }
    }
}