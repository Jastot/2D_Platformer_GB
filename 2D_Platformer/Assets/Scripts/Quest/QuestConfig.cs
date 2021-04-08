using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(menuName = "Create QuestConfig", fileName = "QuestConfig", order = 0)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType questType;
    }

    public enum QuestType
    {
        Switch,
    }
}