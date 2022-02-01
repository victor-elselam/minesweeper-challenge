using Assets.Scripts.Model;
using Assets.Scripts.Presenter;
using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class GameView : MonoBehaviour, IGameView
    {
        public event Action<ICell> OnMark;
        public event Action<ICell> OnFlip;

        [SerializeField] private GridView gridView;
        [SerializeField] private MinesweeperUI minesweeperUI;

        public void Start()
        {
            gridView.OnMark += SendMarked;
            gridView.OnFlip += SendFlipped;
        }
        public void PopulateGrid(BasicGridModel gridModel, GameSettings gameSettings)
        {
            minesweeperUI.SetupUI(gridModel.BombsCount);
            gridView.PopulateGrid(gridModel, gameSettings);
        }

        public void GameEnd(bool win)
        {
            minesweeperUI.GameEnd(win);
        }


        public void SetMarked(ICell cell, bool isMarked) => gridView.SetMarked(cell, isMarked);
        public void SetFlipped(ICell cell) => gridView.SetFlipped(cell);


        private void SendFlipped(ICell cell) => OnFlip?.Invoke(cell);
        private void SendMarked(ICell cell) => OnMark?.Invoke(cell);
    }
}