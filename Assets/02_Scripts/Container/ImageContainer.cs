using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whale
{
    [CreateAssetMenu]
    public class ImageContainer : ScriptableObject
    {
        public List<Image> uiImage = new List<Image>();
    }
}
