using Platformer_2D;
using UnityEngine;

namespace Quest
{
    public class SwitchQuestModel:IQuestModel
    {
        private const string TargetTag = "Player";

        public bool TryComplete(GameObject activator)
        {
            return activator.CompareTag(TargetTag);
        }
    }
}