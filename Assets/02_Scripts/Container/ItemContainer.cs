using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    [CreateAssetMenu(fileName = "New item", menuName = "New Item/item")]
    public class ItemContainer : ScriptableObject
    {
        public GameObject itemPrefab;
        public string itemName;
        public int key;

        public static ItemContainer instance;

        private void Awake()
        {
            instance = this;
        }

    }
}

