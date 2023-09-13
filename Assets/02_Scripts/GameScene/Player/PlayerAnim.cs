using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Jump
    }

    public PlayerState playerState;

    private static PlayerAnim instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static PlayerAnim Instance { get { return instance; } }

    public void ChangeState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                {
                    playerState = PlayerState.Idle;
                    break;
                }
            case PlayerState.Walk:
                {
                    playerState = PlayerState.Walk;
                    break;
                }
            case PlayerState.Jump:
                {
                    playerState = PlayerState.Jump;
                    break;
                }
        }
    }
}
