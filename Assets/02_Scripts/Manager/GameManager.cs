using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace whale
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gm;

        [Header("Prefabs")]
        [SerializeField] GameObject PlayerPref;

        [Header("StartPos")]
        [SerializeField] Transform p1StartPos;
        [SerializeField] Transform p2StartPos;

        private void Awake()
        {
            gm = this;
        }

        private void Start()
        {
            CreatePlayer(1);
        }
        public GameObject CreatePlayer(int a)
        {
            switch (a)
            {
                case 0:
                    Debug.Log("Net Error");
                    return null;
                    break;
                case 1:
                    GameObject Player1 = Instantiate(PlayerPref, p1StartPos);
                    Player1.transform.SetParent(null, false);
                    Player1.gameObject.name = "Player1";
                    FreeLockCamera aa = GameObject.Find("FreeLook Camera").GetComponent<FreeLockCamera>();
                    aa.cfl.Follow = Player1.transform;
                    aa.cfl.LookAt = Player1.transform;
                    return null;
                    break;
                case 2:
                    GameObject Player2 = Instantiate(PlayerPref, p2StartPos);
                    Player2.transform.SetParent(null, false);
                    Player2.gameObject.name = "Player2";
                    return Player2;
                    break;
            }
            return null;
        }
    }
}