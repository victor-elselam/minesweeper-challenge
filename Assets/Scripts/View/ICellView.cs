using Assets.Scripts.Model;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.View
{
    public interface ICellView
    {
        UnityEvent<ICell> OnMark { get; }
        UnityEvent<ICell> OnFlip { get; }
        void SetPosition(int x, int y);
        void SetModel(ICell cell);
        void Flip();
        void Mark();
    }
}