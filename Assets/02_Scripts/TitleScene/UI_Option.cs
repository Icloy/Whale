using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class UI_Option : MonoBehaviour
    {
        [Header("All")]
        public GameObject allOption;

        [Header("Tab")]
        [SerializeField] GameObject gameTab;
        [SerializeField] GameObject soundTab;
        [SerializeField] GameObject elseTab;

        OptionState optionState;

        private void Start()
        {
            optionState = OptionState.Game;
        }

        private void ClearTab()
        {
            optionState = OptionState.Game;
            gameTab.SetActive(false);
            soundTab.SetActive(false);
            elseTab.SetActive(false);
        }

        #region Click
        public void GameOption()
        {
            optionState = OptionState.Game;
            ClearTab();
            gameTab.SetActive(true);

        }

        public void SoundOption()
        {
            optionState = OptionState.Sound;
            ClearTab();
            soundTab.SetActive(true);

        }

        public void ElseOption()
        {
            optionState = OptionState.Else;
            ClearTab();
            elseTab.SetActive(true);

        }

        public void BackButton()
        {
            ClearTab();
            allOption.SetActive(false);
            MainManager.Instance.uiManager.ui_TitleButton.allTitleButton.SetActive(true);
        }
        #endregion

        enum OptionState
        {
            Game,
            Sound,
            Else
        }
    }
}
