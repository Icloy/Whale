using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace whale
{
    public class GameManager : MonoBehaviour
    {
        static GameManager gm;

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
            switch (MainManager.Instance.statusContainer.userNum)
            {
                case 0 :
                    Debug.Log("Net Error");
                    break;
                case 1:
                    GameObject Player1 = Instantiate(PlayerPref, p1StartPos);
                    Player1.transform.SetParent(null, false);
                    break;
                case 2:
                    GameObject Player2 = Instantiate(PlayerPref, p2StartPos);
                    Player2.transform.SetParent(null, false);
                    break;
            }
        }
    }
}