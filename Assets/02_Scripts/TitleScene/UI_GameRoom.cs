using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace whale
{
    public class UI_GameRoom : MonoBehaviour
    {
        [Header("All")]
        public GameObject allGameRoom;
        
        [Header("Object")]
        public GameObject popUP;
        [SerializeField] GameObject joinPopUp;
        [SerializeField] GameObject gameRoom;

        [Header("Join")]
        [SerializeField] TMP_InputField userName;
        [SerializeField] TMP_InputField roomIp;

        [Header("GameLobby")]
        [SerializeField] GameObject userInfo;


        void ClearPanel()
        {
            popUP.SetActive(false);
            joinPopUp.SetActive(false);
            gameRoom.SetActive(false);
        }

        #region  Click
        public void PopUP_JoinRoom()
        {
            ClearPanel();
            joinPopUp.SetActive(true);
        }
        public void PopUP_CreateRoom()
        {
            ClearPanel();
            gameRoom.SetActive(true);
        }
        public void PopUP_Cancel()
        {
            ClearPanel();
            allGameRoom.SetActive(false);
            MainManager.Instance.uiManager.ui_TitleButton.allTitleButton.SetActive(true);
        }

        public void Join_Cancel()
        {
            userName.text = "";
            roomIp.text = "";
            ClearPanel();
            popUP.SetActive(true);
        }
        public void Room_RefreshButton()
        {

        }

        public void Room_GameStart()
        {
            MainManager.Instance.loadingManager.LoadScene("03_GameScene");

        }
        #endregion
    }
}
