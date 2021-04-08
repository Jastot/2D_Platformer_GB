using System;
using System.Collections.Generic;
using System.Linq;
using Platformer_2D;
using UnityEngine;

namespace Quest
{
    public class QuestsConfigurator: MonoBehaviour
    {
        [SerializeField] private QuestObjectView _singleQuestView;

        [SerializeField] private QuestStoryConfig[] _questStoryConfigs;
        [SerializeField] private QuestObjectView[] _questObjects;

        #region Fields

        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>
        {
           { QuestType.Switch, () => new SwitchQuestModel() },
        };

        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>
        {
            { QuestStoryType.Common, questCollection => new QuestStory(questCollection) },
            { QuestStoryType.Resettable, questCollection => new ResettableQuestStory(questCollection) },
        };

        private List<IQuestStory> _questStories;
        private Quest _singleQuest;

        #endregion

        #region Unity methods

        private void Start()
        {
            _singleQuest = new Quest(_singleQuestView, new SwitchQuestModel());
            _singleQuest.Reset();

            _questStories = new List<IQuestStory>();
            foreach (var questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }
        }

        private void OnDestroy()
        {
            foreach (var questStory in _questStories)
            {
                questStory.Dispose();
            }
            _questStories.Clear();
        }

        #endregion


        #region Methods

        private IQuestStory CreateQuestStory(QuestStoryConfig config)
        {
            var quests = new List<IQuest>();
            foreach (var questConfig in config.quests)
            {
                // создаём квест на основе данных из ScriptableObject
                var quest = CreateQuest(questConfig);
                if (quest == null) continue;
                quests.Add(quest);
            }
            // какая логика будет у цепочки определяем по типу QuestStoryType
            return _questStoryFactories[config.questStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            var questId = config.id;
            var questView = _questObjects.FirstOrDefault(value => value.Id == config.id);
            if (questView == null)
            {
                // пытаемся найти представление для квеста
                Debug.LogWarning($"QuestsConfigurator :: Start : Can't find view of quest {questId.ToString()}");
                return null;
            }

            if (_questFactories.TryGetValue(config.questType, out var factory))
            {
                // пытаемся создать модель для квеста по типу QuestType
                var questModel = factory.Invoke();
                return new Quest(questView, questModel);
            }

            Debug.LogWarning($"QuestsConfigurator :: Start : Can't create model for quest {questId.ToString()}");
            return null;
        }

        #endregion
    }
}