using Platformer_2D;
using UnityEngine;
using Utils;

namespace Enemies.Gun.BulletsForCannon
{
    public class BulletLogic: IExecute
    {
        private float _radius = 0.3f;
        private Vector3 _velocity;

        private float _groundLevel = 0;
        private float _g = -10;

        private BulletView _view;

        public BulletLogic(BulletView view)
        {
            _view = view;
            _view.SetVisible(false);
        }

        public void Execute(float deltaTime)
        {
            if (IsGrounded())
            {
                SetVelocity(_velocity.Change(y: -_velocity.y));
                _view.Transform.position = _view.Transform.position.Change(y: _groundLevel+_radius);
            }
            else
            {
                SetVelocity(_velocity + Vector3.up * _g * Time.deltaTime);
                _view.Transform.position += _velocity * Time.deltaTime;
            }
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view.Transform.position = position;
            SetVelocity(velocity);
            _view.SetVisible(true);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _view.Transform.rotation = Quaternion.AngleAxis(angle, axis);

        }

        private bool IsGrounded()
        {
            return _view.Transform.position.y <= _groundLevel + _radius + float.Epsilon && _velocity.y <= 0;
        }

    }
}