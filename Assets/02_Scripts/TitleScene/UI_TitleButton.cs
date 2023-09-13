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
            //æ¿¿¸»Ø
        }

        public void OptionButtonClick()
        {
            MainManager.Instance.uiManager.ui_option.allOption.SetActive(true);
            allTitleButton.SetActive(false);
        }

        public void ExitButtonClick()
        {
            Application.Quit();
        }
        #endregion
    }
}
