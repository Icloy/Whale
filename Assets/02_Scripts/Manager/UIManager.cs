using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale{
    public class UIManager : MonoBehaviour
    {
        [Header("TitleScene")]
        public UI_TitleButton ui_TitleButton;
        public UI_Option ui_option;


        private void Awake()
        {
            switch (MainManager.Instance.gameSceneState)
            {
                case GameSceneState.Title :
                    ui_TitleButton = GameObject.Find("UI_TitleButton").GetComponent<UI_TitleButton>();
                    ui_option = GameObject.Find("UI_Option").GetComponent<UI_Option>();
                    break;
                case GameSceneState.GameScene:
                    
                    break;
            }
        }
    }
}
