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
        [SerializeField] GameObject joinPopup;
        [SerializeField] GameObject gameRoom;
        [SerializeField] GameObject exceptionPopup;

        [Header("Join")]
        [SerializeField] TMP_InputField userName;
        [SerializeField] TMP_InputField roomIp;

        [Header("Exception")]
        [SerializeField] TMP_Text content;


        [Header("GameLobby")]
        [SerializeField] GameObject userInfo;

        private void Start()
        {
            
        }
        void ClearPanel()
        {
            popUP.SetActive(false);
            joinPopup.SetActive(false);
            gameRoom.SetActive(false);
            exceptionPopup.SetActive(false);
        }

        #region  Click
        public void PopUP_JoinRoom()
        {
            ClearPanel();
            joinPopup.SetActive(true);
            TitleManager.s.RoomEnter();
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

        public void Join_Join()
        {
            if(userName.text.Length <= 0)
            {
                content.text = MainManager.Instance.languageContainer.titleUIText[0];
                exceptionPopup.SetActive(true);
                return;
            }
            else if(roomIp.text.Length <= 0)
            {
                content.text = MainManager.Instance.languageContainer.titleUIText[1];
                exceptionPopup.SetActive(true);
                return;
            }
            MainManager.Instance.statusContainer.userName = userName.text;

        }

        public void Join_Cancel() //RoomCancel도 이거 사용
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

        public void Exception_Ok()
        {
            exceptionPopup.SetActive(false);
        }
        #endregion
    }
}
