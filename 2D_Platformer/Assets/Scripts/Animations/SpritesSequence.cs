using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Platformer_2D
{
    [Serializable]
    public class SpritesSequence
    {
        public Track Track;
        public List<Sprite> Sprites = new List<Sprite>();
    }
}