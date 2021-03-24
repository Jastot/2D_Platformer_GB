using System;
using Pathfinding;
using Player;
using UnityEngine;

namespace PatrollAI
{
    public class StalkerAI
    {
        #region Fields
        private readonly PlayerView _view;
        private readonly StalkerAIModel _model;
        private readonly Seeker _seeker;
        private readonly Transform _target;
        #endregion
  
        #region Class life cycles
  
        public StalkerAI(PlayerView view, StalkerAIModel model, Seeker seeker, Transform target)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
            _seeker = seeker != null ? seeker : throw new ArgumentNullException(nameof(seeker));
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));
        }
  
        #endregion

        #region Methods

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Struct.Transform.position) * Time.fixedDeltaTime;
            _view.Struct.Rigidbody2D.velocity = newVelocity;
        }
  
        public void RecalculatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_view.Struct.Rigidbody2D.position, _target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;
            _model.UpdatePath(p);
        }
  
        #endregion
    }

}