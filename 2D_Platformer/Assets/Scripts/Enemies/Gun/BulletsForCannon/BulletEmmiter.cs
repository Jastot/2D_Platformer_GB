using System.Collections.Generic;
using Platformer_2D;
using UnityEngine;

namespace Enemies.Gun.BulletsForCannon
{
    public class BulletEmmiter: IExecute
    {
        private const float _delay = 1;
        private const float _startSpeed = 5;

        private List<BulletLogic> _bullets = new List<BulletLogic>();
        private Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet;

        public BulletEmmiter(List<BulletView> bulletViews, Transform transform)
        {
            _transform = transform;
            foreach (var bulletView in bulletViews)
            {
                _bullets.Add(new BulletLogic(bulletView));
            }
        }

        public void Execute(float deltaTime)
        {
            if(_timeTillNextBullet > 0)
            {
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position, _transform.up * _startSpeed);
                _currentIndex++;
                if (_currentIndex >= _bullets.Count) _currentIndex = 0;
            }
        }

    }
}