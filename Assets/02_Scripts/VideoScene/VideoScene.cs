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
        [SerializeField] bool outro;

        private void Start()
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }

        void OnVideoEnd(VideoPlayer vp)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
            if (outro)
            {
                MainManager.Instance.loadingManager.LoadScene("02_TitleScene");
                return;
            }
            //MainManager.Instance.loadingManager.LoadScene("03_GameScene");
        }
    }
}