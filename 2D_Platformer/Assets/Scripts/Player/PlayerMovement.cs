using Enums;
using Managers;
using Platformer_2D;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerMovement: ILateExecute
   {
       private const string _verticalAxisName = "Vertical";
       private const string _horizontalAxisName = "Horizontal";

       private const float _animationsSpeed = 10;
       private const float _walkSpeed = 150;
       private const float _jumpForse = 350;
       private const float _jumpThresh = 0.1f;
       private const float _flyThresh = 1f;
       private const float _movingThresh = 0.1f;
       
       private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _doJump;
        private float _goSideWay = 0;


        private ObjectOnSceneView _view;
        private SpriteAnimator _spriteAnimator;
        private readonly Camera _camera;
        private ContactPoller _contactsPoller;

        public PlayerMovement(ObjectOnSceneView view, SpriteAnimator spriteAnimator, Camera camera)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
            _camera = camera;
            _contactsPoller = new ContactPoller(_view.Collider2D);
        }
        public void LateExecute(float deltaTime)
        {
            _doJump = Input.GetAxis(_verticalAxisName) > 0;
            _goSideWay = Input.GetAxis(_horizontalAxisName);
            _contactsPoller.Execute(deltaTime);

            var walks = Mathf.Abs(_goSideWay) > _movingThresh;

            if(walks) _view.SpriteRenderer.flipX = _goSideWay < 0;
            var newVelocity = 0f;
            if (walks && 
                (_goSideWay > 0 || !_contactsPoller.HasLeftContacts) && 
                (_goSideWay < 0 || !_contactsPoller.HasRightContacts))
            {
                newVelocity = Time.fixedDeltaTime * _walkSpeed * 
                              (_goSideWay < 0 ? -1 : 1);
            }
            _view.Rigidbody2D.velocity = _view.Rigidbody2D.velocity.Change(
                x: newVelocity);
            if (_contactsPoller.IsGrounded && _doJump && 
                Mathf.Abs(_view.Rigidbody2D.velocity.y) <= _jumpThresh)
            {
                _view.Rigidbody2D.AddForce(Vector3.up * _jumpForse);
            }

            //animations
            if (_contactsPoller.IsGrounded)
            {
                var track = walks ? Track.Run:Track.Idle;
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true, 
                    _animationsSpeed);
            }
            else if(Mathf.Abs(_view.Rigidbody2D.velocity.y) > _flyThresh)
            {
                var track = Track.Jump;
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true, 
                    _animationsSpeed);
            }
            _camera.transform.position = _camera.transform.position.Change(_view.Transform.position.x);
            _camera.transform.position = _camera.transform.position.Change(y: _view.Transform.position.y);
            _camera.nearClipPlane = 0; 
        } 
   }
}