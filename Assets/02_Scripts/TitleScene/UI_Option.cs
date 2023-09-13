using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class UI_Option : MonoBehaviour
    {
        [Header("All")]
        public GameObject allOption;

        OptionState optionState;

        private void Start()
        {
            optionState = OptionState.Game;
        }

        #region Click
        public void GameOption()
        {
            optionState = OptionState.Game;

        }

        public void SoundOption()
        {
            optionState = OptionState.Sound;

        }

        public void ElseOption()
        {
            optionState = OptionState.Else;

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
