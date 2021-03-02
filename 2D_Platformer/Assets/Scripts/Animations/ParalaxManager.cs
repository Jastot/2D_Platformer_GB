using System.Collections.Generic;
using UnityEngine;

namespace Platformer_2D
{
    public class ParalaxManager: IExecute
    {
        private Camera _camera;
        private List<Transform> _backs;
        //private GameObject _backs;
        private List<Vector3> _backStartPosition;
        private Vector3 _cameraStartPosition;
        private const float _coef = 0.5f;


        public ParalaxManager(Camera camera,List<Transform> back)
        {
            _camera = camera;
            _backs = back;
            _backStartPosition = new List<Vector3>();
            foreach (var localBack in _backs)
            {
                _backStartPosition.Add(localBack.position);
            }
            _cameraStartPosition = _camera.transform.position;
        }

        public void Execute(float deltaTime)
        {
            var i = 0;
            foreach (var localBack in _backs)
            {
                localBack.transform.position = _backStartPosition[i] +
                                               (_camera.transform.position - _cameraStartPosition) * _coef;
                i++;
            }

        }
    }
}