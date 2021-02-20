using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Player;
using UnityEngine;

namespace Platformer_2D
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteRenderer _back;
        [SerializeField] private GameObject Player;
        [SerializeField] private Transform PlayersTransform;
        [SerializeField] private SpriteRenderer PlayersSpriteRenderer;
        [SerializeField] private Rigidbody2D PlayersRigidbody2D;
        [SerializeField] private Collider2D PlayersCollider2D;
        [SerializeField] private SpriteAnimationConfig _spriteAnimationConfig;
        private Controllers _controllers;

        private void Start()
        {

            var PlayersView = new PlayerView()
            {
                Player = Player,
                Transform = PlayersTransform,
                SpriteRenderer = PlayersSpriteRenderer,
                Rigidbody2D = PlayersRigidbody2D,
                Collider2D = PlayersCollider2D,
            };
            
            //SpriteAnimationConfig config = Resources.Load<SpriteAnimationConfig>("SpriteAnimationsConfig");
            
            var spriteAnimator = new SpriteAnimator(_spriteAnimationConfig);
            
            _controllers = new Controllers();
            //_controllers.Add(new SpriteAnimator());
            _controllers.Add(new ParalaxManager(_camera.transform, _back.transform));
            //SomeConfig config = Resources.Load("SomeConfig", typeof(SomeConfig))as   SomeConfig;
            //load some configs here <3>

            //_someManager = new SomeManager(config);
            //create some logic managers here for tests <4>
            spriteAnimator.StartAnimation(PlayersView.SpriteRenderer, Track.Run, true, 10);
            _controllers.Add(spriteAnimator);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }


        private void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.CleanData();
        }
    }
}
