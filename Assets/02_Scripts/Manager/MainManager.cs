using UnityEngine;

namespace whale
{
    public class MainManager : SingletonScript<MainManager>
    {
        [Header("Manager")]
        public UIManager uiManager;
        public NetworkManager networkManager;
        public NetGameManager netGameManager;
        public TitleManager titleManager;
        public LoadingManager loadingManager;

        [Header("GameScene")]
        public PlayerAnim playerAnim;

        [Header("Container")]
        public ImageContainer imageContainer;
        public StatusContainer statusContainer;
        public AudioContainer audioContainer;
        public LanguageContainer languageContainer;

        [Header("Other")]
        public GameSceneState gameSceneState;
    }
    public enum GameSceneState
    {
        Title,
        GameScene
    }
}