using MNF;
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
                MainManager.Instance.loadingManager.LoadScene("04_Ending");
                return;
            }
            else
            {
                MainManager.Instance.titleManager.OnClick_Login(MainManager.Instance.statusContainer.userName);
                MainManager.Instance.titleManager.OnClick_Start();
            }
        }
    }
}