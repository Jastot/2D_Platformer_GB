using System;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;

namespace Platformer_2D
{
    public class LevelCompleteController: ICleanData
    {
        private Vector3 _startPosition;
        private PlayerView _characterView;
        private List<ObjectView> _deathZones;
        private List<ObjectView> _winZones;

        public LevelCompleteController(PlayerView characterView, List<ObjectView> deathZones, List<ObjectView> winZones)
        {
            _startPosition = characterView.Struct.Transform.position;
            characterView.OnLevelObjectContact += OnLevelObjectContact;

            _characterView = characterView;
            _deathZones = deathZones;
            _winZones = winZones;
        }

        private void OnLevelObjectContact(ObjectView contactView)
        {
            if(_deathZones.Contains(contactView))
            {
                _characterView.Struct.Transform.position = _startPosition;
            }
        }

        public void CleanData()
        {
            _characterView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}