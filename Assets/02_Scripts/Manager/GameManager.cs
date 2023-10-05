using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class GameManager : MonoBehaviour
    {
        
        static GameManager gm;


        private void Awake()
        {
            gm = this;
        }

        private void Start()
        {
            
        }
    }
}