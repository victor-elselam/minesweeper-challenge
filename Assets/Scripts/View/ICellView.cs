using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    public interface ICellView
    {
        CellUnityEvent OnMark { get; }
        CellUnityEvent OnFlip { get; }
        void SetPosition(int x, int y);
        void SetModel(ICell cell);
        void InputToFlip();
        void InputToMark();

        void Destroy();
    }
}