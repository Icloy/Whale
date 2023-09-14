using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class UI_TitleButton : MonoBehaviour
    {
        [Header("All")]
        public GameObject allTitleButton;

        #region Click
        public void GameStartButtonClick()
        {
            MainManager.Instance.uiManager.ui_GameRoom.allGameRoom.SetActive(true);
            allTitleButton.SetActive(false);
        }

        public void OptionButtonClick()
        {
            MainManager.Instance.uiManager.ui_Option.allOption.SetActive(true);
            allTitleButton.SetActive(false);
        }

        public void ExitButtonClick()
        {
            Application.Quit();
        }
        #endregion
    }
}
