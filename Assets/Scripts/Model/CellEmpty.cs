using System;

namespace Assets.Scripts.Model
{
    public class CellEmpty : ICell
    {
        public event Action<bool> OnMark;
        public event Action OnFlip;
        public bool IsMarked => isMarked;
        public bool IsFlipped => isFlipped;

        public int X { get; private set; }

        public int Y { get; private set; }

        public int TouchingBombs { get; set; }
        private bool isMarked;
        private bool isFlipped;

        public CellEmpty(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetFlipped()
        {
            isFlipped = true;
            OnFlip?.Invoke();
        }

        public void SetMarked(bool marked)
        {
            isMarked = marked;
            OnMark?.Invoke(marked);
        }
    }
}