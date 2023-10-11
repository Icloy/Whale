using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class StatusContainer : ScriptableObject
    {
        public string userName;
        public int userNum;
        public float moveSpeed;
        

        private void Awake()
        {
            moveSpeed = 10f;
            userNum = 0; //default:0 p1: 1 p2: 2
        }
    }
}
