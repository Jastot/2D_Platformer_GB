using UnityEngine;

namespace Platformer_2D
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}