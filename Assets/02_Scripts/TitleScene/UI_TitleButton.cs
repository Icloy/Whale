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
            allTitleButton.SetActive(false);
            MainManager.Instance.uiManager.ui_GameRoom.allGameRoom.SetActive(true);
            MainManager.Instance.uiManager.ui_GameRoom.popUP.SetActive(true);
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
