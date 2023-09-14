using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class MainManager : Singleton<MainManager>
    {
        [Header("TitleScene")]
        public UIManager uiManager;
        public NetworkManager networkManager;
        public NetGameManager netGameManager;

        [Header("GameScene")]
        public PlayerAnim playerAnim;

        [Header("Container")]
        public ImageContainer imageContainer;
        public StatusContainer statusContainer;
        public AudioContainer audioContainer;

        [Header("Other")]
        public GameSceneState gameSceneState;
    }

    public enum GameSceneState
    {
        Title,
        GameScene
    }
}