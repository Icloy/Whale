using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class UI_GameRoom : MonoBehaviour
    {
        [Header("All")]
        public GameObject allGameRoom;



        #region  Click
        public void RefreshButton()
        {

        }

        public void CancleButton()
        {
            allGameRoom.SetActive(false);
            MainManager.Instance.uiManager.ui_TitleButton.allTitleButton.SetActive(true);
        }

        public void GameStart()
        {

        }
        #endregion
    }
}
