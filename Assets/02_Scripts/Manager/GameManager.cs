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

        [Header("StartPos")]
        bool isCreateP1;
        private void Awake()
        {
            gm = this;
        }

        private void Start()
        {
            //CreatePlayer(1, MainManager.Instance.statusContainer.userName);
        }
        public void CreatePlayer(int a, string name)
        {
            switch (a)
            {
                case 0:
                    Debug.Log("Net Error");
                    break;
                case 1:
                    if (isCreateP1) break;
                    GameObject Player1 = Instantiate(PlayerPref, p1StartPos);
                    Player1.transform.SetParent(null, false);
                    FreeLockCamera aa = GameObject.Find("FreeLook Camera").GetComponent<FreeLockCamera>();
                    aa.cfl.Follow = Player1.transform;
                    aa.cfl.LookAt = Player1.transform;
                    Player1.name = name;
                    isCreateP1 = true;
                    break;
                case 2:
                    GameObject Player2 = Instantiate(PlayerPref, p2StartPos);
                    Player2.transform.SetParent(null, false);
                    Player2.name = name;
                    Destroy(Player2.GetComponent<Player>());
                    Destroy(Player2.GetComponent<PickUp>());
                    break;
            }
        }
    }
}