using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    [Serializable]
    public class GAME_START
    {
        public string USER = "";
        public int DATA = 0;
    }

    [Serializable]
    public class TANK_FIRE
    {
        public string USER = "";
        public int DATA = 0;
        public string Power = "";
        public string Position = "";
    }

    [Serializable]
    public class Object_Interaction
    {
        public int USER = 0; // 0: empty 1: player1 2: player2
        public int DATA = 0; //서버상 데이터 분류
        public int STATE = 0; // 0:default 1: action
        public string WHERE = ""; //어떤 오브젝트인지
    }
}