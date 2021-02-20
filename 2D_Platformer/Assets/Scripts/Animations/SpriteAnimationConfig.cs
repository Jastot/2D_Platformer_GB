using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer_2D
{
    [CreateAssetMenu(fileName = "SpriteAnimationConfig", menuName = "Configs/SpriteAnimationConfig")]
    public class SpriteAnimationConfig: ScriptableObject
    {
        public List<SpritesSequence> Sequences = new List<SpritesSequence>();
    }
}