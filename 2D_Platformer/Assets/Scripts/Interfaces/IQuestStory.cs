using System;

namespace Platformer_2D
{
    public interface IQuestStory: IDisposable
    {
        bool IsDone { get; }
    }
}