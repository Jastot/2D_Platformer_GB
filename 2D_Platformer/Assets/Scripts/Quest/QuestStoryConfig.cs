using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(menuName = "QuestStoryConfig", fileName = "QuestStoryConfig", order = 0)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType questStoryType;
    }

    public enum QuestStoryType
    {
        Common,
        Resettable
    }

}