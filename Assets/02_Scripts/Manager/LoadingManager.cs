using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace whale
{
    public class LoadingManager : MonoBehaviour
    {
        public string nextScene;

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            /*nextScene = sceneName;
            StartCoroutine(LoadScene());*/
        }

        public void LoadSceneAsync(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        IEnumerator LoadScene()
        {
            yield return null;
            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;
            float timer = 0.0f;
            while (!op.isDone)
            {
                yield return null;
                timer += Time.deltaTime;
            }
            op.allowSceneActivation = true;
        }
    }
}
