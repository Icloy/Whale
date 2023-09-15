using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class LanguageContainer : ScriptableObject
    {
        [TextArea]
        public List<string> titleUIText = new List<string>();  //title UI
    }
}
