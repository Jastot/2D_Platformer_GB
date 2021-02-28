using Enums;
using Platformer_2D;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerMovement: IExecute
    { 
        private const float _walkSpeed = 3f;
        private const float _animationsSpeed = 10f;
        private const float _jumpStartSpeed = 8f;
        private const float _movingThresh = 0.1f;
        private const float _flyThresh = 1f;
        private const float _groundLevel = 0.5f;
        private const float _g = -10f;       
            
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity;
        private bool _doJump;
        private float _xAxisInput;

        private PlayerView _view;
        private SpriteAnimator _spriteAnimator;
        private readonly Camera _camera;

        public PlayerMovement(PlayerView view, SpriteAnimator spriteAnimator, Camera camera)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
            _camera = camera;
        }

        private void GoSideWay()
        {
            _view.Transform.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _view.Transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _view.Transform.position.y <= _groundLevel + float.Epsilon && _yVelocity<=0;
        }
        
        public void Execute(float deltaTime)
        {
            _doJump = Input.GetAxis("Vertical") > 0;
            _xAxisInput = Input.GetAxis("Horizontal");
            var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

            if (IsGrounded())
            {
                //walking
                if (goSideWay) GoSideWay();
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, goSideWay?Track.Run:Track.Idle, true, _animationsSpeed);

                //start jump
                if (_doJump && _yVelocity == 0)
                {
                    
                    _yVelocity = _jumpStartSpeed;
                }
                //stop jump
                else if(_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view.Transform.position = _view.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                //flying
                if (goSideWay) GoSideWay();
                if (Mathf.Abs(_yVelocity) > _flyThresh)
                {
                    _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.Jump, true, _animationsSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _view.Transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
            //так. не понимаю почему у камеры не меняется позиция....
            _camera.transform.position.Change(_view.Transform.position.x);
            _camera.transform.position.Change(y: _view.Transform.position.y);
            _camera.nearClipPlane = 0;
        }
    }
}