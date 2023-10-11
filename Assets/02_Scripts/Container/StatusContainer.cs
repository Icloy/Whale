using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class StatusContainer : ScriptableObject
    {
        public string userName;
        public float moveSpeed;

        private void Awake()
        {
            moveSpeed = 10f;
        }
    }
}
