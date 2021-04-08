using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Quest
{
    public class QuestObjectView: ObjectView
    {
        public int Id => _id;
        [SerializeField] private Color _completedColor;
        [SerializeField] private int _id;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        

        private Color _defaultColor;

        #region Unity methods

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
        }

        #endregion

        #region Methods

        public void ProcessComplete()
        {
            _spriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            _spriteRenderer.color = _defaultColor;
        }

        #endregion
    }
}