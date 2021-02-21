using System.Collections.Generic;
using UnityEngine;

namespace Platformer_2D
{
    public class ParalaxManager: IExecute
    {
        private Camera _camera;
        //private Dictionary<int,GameObject> _backs;
        private GameObject _backs;
        private Vector3 _backStartPosition;
        private Vector3 _cameraStartPosition;
        private const float _coef = 0.3f;
        private List<BoxCollider2D> _collider2Ds;
        private GameObject _backPrefab;
        
       
        private float _backPositionCoef = 1.8f;

        public ParalaxManager(Camera camera, GameObject back, GameObject backPrefab)
        {
            _camera = camera;
            _backPrefab = backPrefab;
            //_backs = new Dictionary<int, GameObject>();
            //_backs.Add(0,back); 
            _backs = back;
            //_backStartPosition = _backs[0].transform.position;
            _backStartPosition = _backs.transform.position;
            _cameraStartPosition = _camera.transform.position;
            _collider2Ds = new List<BoxCollider2D>();
        }

        public void Execute(float deltaTime)
        {
            //SearchForDots();
            //foreach (var back in _backs)
            //{
               // var vectorN = new Vector3(back.Key, 0, 0);
              //  back.Value.transform.position = (_backStartPosition * _backPositionCoef + vectorN) + (_camera.transform.position - _cameraStartPosition) * _coef;
                
            //}
            _backs.transform.position = _backStartPosition+ (_camera.transform.position - _cameraStartPosition) * _coef;

        }

        // public void SearchForDots()
        // {
        //     foreach (GameObject target in _backs.Values)
        //     {
        //         Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        //         _collider2Ds.AddRange(target.GetComponents<BoxCollider2D>());
        //         //3 зоны экрана
        //         //  лево - копия
        //         //  Центр покоя
        //         //  право - копия
        //         if (GeometryUtility.TestPlanesAABB(planes, _collider2Ds[0].bounds) 
        //             || GeometryUtility.TestPlanesAABB(planes, _collider2Ds[1].bounds)
        //             || GeometryUtility.TestPlanesAABB(planes, _collider2Ds[2].bounds))
        //         {
        //             //зона клонирования и проверки - лево
        //             // if (GeometryUtility.TestPlanesAABB(planes, _collider2Ds[0].bounds))
        //             // {
        //             //     Transform transform;
        //             //     transform.position = _backStartPosition * _backPositionCoef
        //             //     _backs.Add(_backs.Count,GameObject.Instantiate(_backPrefab,));
        //             // }
        //             
        //             //зона клонирования и проверки - право
        //             if (GeometryUtility.TestPlanesAABB(planes, _collider2Ds[2].bounds))
        //             {
        //                 Transform transform = new RectTransform();
        //                 var vectorN = new Vector3(_backs.Count, 0, 0);
        //                 transform.position = _backStartPosition * _backPositionCoef + vectorN;
        //                 _backs.Add(_backs.Count,GameObject.Instantiate(_backPrefab,transform));
        //             }
        //             
        //         }
        //         _collider2Ds.Clear();
        //     }
        //}
    }
}