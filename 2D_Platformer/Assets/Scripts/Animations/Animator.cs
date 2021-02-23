using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Platformer_2D
{
    public class Animator: IExecute
    {
        public Track Track;
        public List<Sprite> Sprites;
        public bool Loop = false;
        public float Speed = 10;
        public float Counter = 0;
        public bool Sleeps;
        

        public void Execute(float deltaTime)
        {
            if (Sleeps) return;
            Counter += deltaTime * Speed;
            if (Loop)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count - 1;
                Sleeps = true;
            }
        }
    }
}