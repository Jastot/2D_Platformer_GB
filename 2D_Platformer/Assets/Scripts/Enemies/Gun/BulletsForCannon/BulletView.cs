using Managers;
using Player;
using UnityEngine;

namespace Enemies.Gun.BulletsForCannon
{
    public class BulletView: ObjectOnSceneView
    {
        public TrailRenderer _trail;

        public void SetVisible(bool visible)
        {
            if (_trail) _trail.enabled = visible;
            if (_trail) _trail.Clear();
            SpriteRenderer.enabled = visible;
        }
    }
}