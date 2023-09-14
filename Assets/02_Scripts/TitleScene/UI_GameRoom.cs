using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class UI_GameRoom : MonoBehaviour
    {
        [Header("All")]
        public GameObject allGameRoom;

        [Header("GameLobby")]
        [SerializeField] GameObject userInfo;
        public GameObject gameRoom;
        public GameObject popUP;

        void ClearPanel()
        {
            gameRoom.SetActive(false);
            popUP.SetActive(false);
        }

        #region  Click
        public void PopUP_JoinRoom()
        {
            ClearPanel();
            gameRoom.SetActive(true);
        }
        public void PopUP_CreateRoom()
        {
            ClearPanel();
            gameRoom.SetActive(true);
        }

        public void Room_RefreshButton()
        {

        }

        public void Room_CancelButton()
        {
            ClearPanel();
            allGameRoom.SetActive(false);
            MainManager.Instance.uiManager.ui_TitleButton.allTitleButton.SetActive(true);
        }

        public void Room_GameStart()
        {

        }
        #endregion
    }
}
