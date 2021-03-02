using System.Collections.Generic;
using Enums;
using Managers;
using Platformer_2D;
using Player;
using UnityEngine;

namespace Coins
{
    public class CoinsManager : ICleanData
    {
        private const float _animationsSpeed = 10;

        private PlayerView _characterView;
        private SpriteAnimator _spriteAnimator;
        private List<ObjectView> _coinViews;

        public CoinsManager(PlayerView PlayerView, 
            List<ObjectView> coinViews, 
            SpriteAnimator spriteAnimator)
        {
            _characterView = PlayerView;
            _spriteAnimator = spriteAnimator;
            _coinViews = coinViews;
            _characterView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (var coinView in coinViews)
            {
                _spriteAnimator.StartAnimation(coinView.Struct.SpriteRenderer, Track.Idle, true, _animationsSpeed);
            }
        }

        private void OnLevelObjectContact(ObjectView contactView)
        {
            if (_coinViews.Contains(contactView))
            {
                _spriteAnimator.StopAnimation(contactView.Struct.SpriteRenderer);
                GameObject.Destroy(contactView.gameObject);
            }
        }


        public void CleanData()
        {
            _characterView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}