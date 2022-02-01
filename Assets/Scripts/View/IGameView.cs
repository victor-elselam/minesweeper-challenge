using Assets.Scripts.Model;
using System;

namespace Assets.Scripts.View
{
    public interface IGameView
    {
        event Action<ICell> OnMark;
        event Action<ICell> OnFlip;

        void PopulateGrid(BasicGridModel gridModel, GameSettings gameSettings);
        void GameEnd(bool win);
        void SetMarked(ICell cell, bool isMarked);
        void SetFlipped(ICell cell);
    }
}