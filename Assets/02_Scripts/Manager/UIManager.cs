using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale{
    public class UIManager : MonoBehaviour
    {
        [Header("TitleScene")]
        public UI_TitleButton ui_TitleButton;
        public UI_Option ui_Option;
        public UI_GameRoom ui_GameRoom;


        private void Awake()
        {
            switch (MainManager.Instance.gameSceneState)
            {
                case GameSceneState.Title :
                    ui_TitleButton = GameObject.Find("UI_TitleButton").GetComponent<UI_TitleButton>();
                    ui_Option = GameObject.Find("UI_Option").GetComponent<UI_Option>();
                    ui_GameRoom = GameObject.Find("UI_GameRoom").GetComponent<UI_GameRoom>();
                    break;
                case GameSceneState.GameScene:
                    
                    break;
            }
        }
    }
}
