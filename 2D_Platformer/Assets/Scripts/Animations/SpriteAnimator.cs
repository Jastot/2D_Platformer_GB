
using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Platformer_2D
{
    public class SpriteAnimator: IDisposable, IExecute
    {
        private SpriteAnimationConfig _config;
        private Dictionary<SpriteRenderer, Animator> _activeAnimations = new Dictionary<SpriteRenderer, Animator>();

        public SpriteAnimator(SpriteAnimationConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, Track track, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleeps = false;
                if(animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animator()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }

        public void Execute(float deltaTime)
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Execute(deltaTime);
                animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }
    }
}