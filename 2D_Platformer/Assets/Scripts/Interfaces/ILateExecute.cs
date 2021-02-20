namespace Platformer_2D
{
    public interface ILateExecute: IController
    {
        void LateExecute(float deltaTime);
    }
}