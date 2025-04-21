/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Enhanced.Dyson.Effects
{
    public class UIEffects : MonoBehaviour
    {
        public float fadeTime = 1f;

        public TextMeshProUGUI fadeAwayText;

        public Effects _effects;

        private void Update()
        {
            if (_effects.isObjectActive)
            {
                if (fadeTime > 0)
                {
                    fadeTime -= Time.deltaTime;
                    fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b,
                        fadeTime);
                }   
            }
        }
    }
}
*/