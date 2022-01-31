using System;

namespace Assets.Scripts.Model
{
    public interface ICell
    {
        event Action<bool> OnMark;
        event Action OnFlip;

        bool IsMarked { get; }
        void SetMarked(bool marked);
        bool IsFlipped { get; }
        void SetFlipped();

        int X { get; }
        int Y { get; }
    }
}