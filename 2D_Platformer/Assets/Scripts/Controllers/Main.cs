using System;
using System.Collections;
using System.Collections.Generic;
using Coins;
using Enemies.Gun.BulletsForCannon;
using Enemies.Gun.Cannon;
using Enums;
using Managers;
using Player;
using UnityEngine;
using Utils;

namespace Platformer_2D
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<Transform> _back;
        [SerializeField] private GameObject Player;
        [SerializeField] private PlayerView PlayerView;
        [SerializeField] private Transform PlayersTransform;
        [SerializeField] private Rigidbody2D playerRigidbody2D;
        [SerializeField] private SpriteRenderer PlayersSpriteRenderer;
        [SerializeField] private Collider2D PlayersCollider2D;
        [SerializeField] private SpriteAnimationConfig _PlayerSpriteAnimationConfig;
        [SerializeField] private SpriteAnimationConfig _coinSpriteAnimationConfig;
        [SerializeField] private List<GameObject> bulletGameObject;
        [SerializeField] private Transform eyeTransform;
        [SerializeField] private Transform muzzleTransform;

        [SerializeField] private List<ObjectView> _winZone;

        [SerializeField] private List<ObjectView> _deathZone;
        
        [SerializeField] private List<ObjectView> coinsGameObjects;
        
        private Controllers _controllers;

        private void Start()
        {
            var PlayersView = new ObjectOnSceneView()
            {
                GameObject = Player,
                Transform = PlayersTransform,
                Rigidbody2D = playerRigidbody2D,
                SpriteRenderer = PlayersSpriteRenderer,
                Collider2D = PlayersCollider2D,
            };
            PlayerView.Struct = PlayersView;
            //SpriteAnimationConfig config = Resources.Load<SpriteAnimationConfig>("SpriteAnimationsConfig");
            
            var spriteAnimator = new SpriteAnimator(_PlayerSpriteAnimationConfig);
            
            _controllers = new Controllers();
            _controllers.Add(new ParalaxManager(_camera, _back));
            _controllers.Add(new PlayerMovement(PlayersView, spriteAnimator,_camera));
            _controllers.Add(new AimingMuzzle(muzzleTransform,PlayersView.Transform));
            
            List<BulletView> _bulletViews = new List<BulletView>();
            
            _bulletViews.Add(new BulletView()
            {
                GameObject = bulletGameObject[0],
                Transform = bulletGameObject[0].transform,
                Rigidbody2D = bulletGameObject[0].GetComponent<Rigidbody2D>(),
                SpriteRenderer = bulletGameObject[0].GetComponent<SpriteRenderer>(),
                Collider2D = bulletGameObject[0].GetComponent<Collider2D>(),
                _trail = bulletGameObject[0].GetComponent<TrailRenderer>()
            });
            _bulletViews.Add(new BulletView()
            {
                GameObject = bulletGameObject[1],
                Transform = bulletGameObject[1].transform,
                Rigidbody2D = bulletGameObject[1].GetComponent<Rigidbody2D>(),
                SpriteRenderer = bulletGameObject[1].GetComponent<SpriteRenderer>(),
                Collider2D = bulletGameObject[1].GetComponent<Collider2D>(),
                _trail = bulletGameObject[1].GetComponent<TrailRenderer>()
            });
            _bulletViews.Add(new BulletView()
            {
                GameObject = bulletGameObject[2],
                Transform = bulletGameObject[2].transform,
                Rigidbody2D = bulletGameObject[2].GetComponent<Rigidbody2D>(),
                SpriteRenderer = bulletGameObject[2].GetComponent<SpriteRenderer>(),
                Collider2D = bulletGameObject[2].GetComponent<Collider2D>(),
                _trail = bulletGameObject[2].GetComponent<TrailRenderer>()
            });
            _bulletViews.Add(new BulletView()
            {
                GameObject = bulletGameObject[3],
                Transform = bulletGameObject[3].transform,
                Rigidbody2D = bulletGameObject[3].GetComponent<Rigidbody2D>(),
                SpriteRenderer = bulletGameObject[3].GetComponent<SpriteRenderer>(),
                Collider2D = bulletGameObject[3].GetComponent<Collider2D>(),
                _trail = bulletGameObject[3].GetComponent<TrailRenderer>()
            });
            _bulletViews.Add(new BulletView()
            {
                GameObject = bulletGameObject[4],
                Transform = bulletGameObject[4].transform,
                Rigidbody2D = bulletGameObject[4].GetComponent<Rigidbody2D>(),
                SpriteRenderer = bulletGameObject[4].GetComponent<SpriteRenderer>(),
                Collider2D = bulletGameObject[4].GetComponent<Collider2D>(),
                _trail = bulletGameObject[4].GetComponent<TrailRenderer>()
            });
            _controllers.Add(new BulletEmmiter(_bulletViews,eyeTransform));
            //spriteAnimator.StartAnimation(PlayersView.SpriteRenderer, Track.Run, true, 10);
            _controllers.Add(spriteAnimator);
            var coinSpriteAnimator = new SpriteAnimator(_coinSpriteAnimationConfig);
            foreach (var coin in coinsGameObjects)
            {
                coin.Struct = new ObjectOnSceneView();
                coin.Struct.Transform = coin.transform;
                coin.Struct.Collider2D = coin.GetComponent<Collider2D>();
                coin.Struct.Rigidbody2D = coin.GetComponent<Rigidbody2D>();
                coin.Struct.SpriteRenderer = coin.GetComponent<SpriteRenderer>();
            }
            
            _controllers.Add(new CoinsManager(PlayerView,coinsGameObjects,coinSpriteAnimator));
            _controllers.Add(coinSpriteAnimator);

            _controllers.Add(new LevelCompleteController(PlayerView,_deathZone,_winZone));
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
