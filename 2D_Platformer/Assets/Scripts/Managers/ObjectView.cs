using System;
using UnityEngine;

namespace Managers
{
    public class ObjectView: MonoBehaviour
    {
        public ObjectOnSceneView Struct;
        public Action<ObjectView> OnLevelObjectContact { get; set; }

        void OnTriggerEnter2D(Collider2D collider)
        {
            OnLevelObjectContact?.Invoke(collider.gameObject.GetComponent<ObjectView>());
        }
    }
}