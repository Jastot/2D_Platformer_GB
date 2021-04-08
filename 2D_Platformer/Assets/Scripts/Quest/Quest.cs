using System;
using Managers;
using Platformer_2D;
using Player;

namespace Quest
{
    public class Quest :IQuest
    {
        private readonly QuestObjectView _view;
        private readonly IQuestModel _model;
        private bool _active;
        public event EventHandler<IQuest> Completed;
        public bool IsCompleted { get; private set; }


        public Quest(QuestObjectView view, IQuestModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        }

        private void OnContact(ObjectView arg)
        {
            var completed = _model.TryComplete(arg.gameObject);
            if (completed) Complete();
        }

        private void Complete()
        {
            if (!_active) return;
            _active = false;
            _view.OnLevelObjectContact -= OnContact;
            _view.ProcessComplete();
            OnCompleted();
        }

        private void OnCompleted()
        {
            Completed?.Invoke(this, this);
        }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            _view.OnLevelObjectContact += OnContact;
            _view.ProcessActivate();
        }

        public void Dispose()
        {
            _view.OnLevelObjectContact -= OnContact;
        }
    }
}