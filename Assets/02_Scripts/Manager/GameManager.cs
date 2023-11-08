using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MNF;

namespace whale
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gm;
        public ObjectController objController;
        public ObjText objText;
        public SoundManager soundManager;

        [Header("Prefabs")]
        [SerializeField] GameObject PlayerPref;
        [SerializeField] GameObject PlayerPref2;

        [Header("StartPos")]
        [SerializeField] Transform p1StartPos;
        [SerializeField] Transform p2StartPos;

        [Header("StartPos")]
        bool isPlayer2 = false;

        private void Awake()
        {
            gm = this;

        }

        private void Start()
        {
            CreatePlayer(true, MainManager.Instance.statusContainer.userName);
        }
        public void CreatePlayer(bool islocal, string name)
        {
            if (islocal)
            {
                GameObject Player1 = Instantiate(PlayerPref, p1StartPos);
                Player1.transform.SetParent(null, false);
                FreeLockCamera aa = GameObject.Find("FreeLook Camera").GetComponent<FreeLockCamera>();
                aa.cfl.Follow = Player1.transform;
                aa.cfl.LookAt = Player1.transform;
                Player1.name = name;
            }
            else
            {
                GameObject Player2 = Instantiate(PlayerPref2, p2StartPos);
                Player2.transform.SetParent(null, false);
                Player2.name = name;
                Destroy(Player2.GetComponent<Player>());
                Destroy(Player2.GetComponent<PickUp>());

            } 
        }

    }
}