using System.Collections;
using System.Collections.Generic;
using Enhanced.Dyson.Player;
using UnityEngine;

namespace Enhanced.Dyson.Effects
{
    public class Effects : MonoBehaviour
    {
        public PlayerController _player;
        public UIEffects _ui;
        public GameObject magnesiumText;
        public bool isObjectActive;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Magnesium();    
            }
        }

        private void Magnesium()
        {
            _player.speed = 25;
            magnesiumText.SetActive(true);
            isObjectActive = true;
        }
    }
}
