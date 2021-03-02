using System;
using Coins;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerView: ObjectView
    {
        public Action<ObjectView> OnLevelObjectContact { get; set; }

        void OnTriggerEnter2D(Collider2D collider)
        {
            OnLevelObjectContact?.Invoke(collider.gameObject.GetComponent<ObjectView>());
        }

    }
}