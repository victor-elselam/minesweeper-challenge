using Assets.Scripts.Model;
using Assets.Scripts.Services;
using Assets.Scripts.View;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Presenter
{
    public class MinesweeperPresenter
    {
        private BasicGridModel gridModel;
        private GameSettings gameSettings;
        private WinConditionHandler winCondition;
        private IGameView gameView;
        private IInputService inputService;
        private int bombsCount;

        public MinesweeperPresenter(GameSettings gameSettings, IGameView gameView, IInputService inputService)
        {
            this.gameSettings = gameSettings;
            this.gameView = gameView;
            this.inputService = inputService;
            winCondition = new WinConditionHandler();

            gameView.OnFlip += FlipCell;
            gameView.OnMark += Mark;
            SetupPlaceholderGame();
        }

        ~MinesweeperPresenter()
        {
            gameView.OnFlip -= FlipCell;
            gameView.OnMark -= Mark;
        }

        //this usage of this Task to restart game is not ideal. Probably we would need more layers to deal with the application loop
        public async Task GameEnd(bool win)
        {
            Debug.Log(win ? "Game Win" : "Game Lose");
            gameView.GameEnd(win);
            await Task.Delay(1 * 1000);
            SetupPlaceholderGame();
        }

        public void SetupPlaceholderGame()
        {
            var gridSettings = gameSettings.GridSettings;
            bombsCount = Random.Range(gameSettings.MinBombs, gameSettings.MaxBombs);
            gridModel = new BasicGridModel(gridSettings, bombsCount);
            Setup(gridModel);
        }

        public void SetupGame(IntVector2 firstFlip)
        {
            var gridSettings = gameSettings.GridSettings;
            gridModel = new GridModel(gridSettings, bombsCount, firstFlip);
            Setup(gridModel);

            FlipCell(gridModel.Grid[firstFlip.X, firstFlip.Y]);
        }

        private void Setup(BasicGridModel gridModel)
        {
            gameView.PopulateGrid(gridModel, gameSettings);
        }

        public async void FlipCell(ICell cell)
        {
            inputService.Enable(false);

            //return to don't allow flipping a marked cell
            if (cell.IsMarked)
            {
                inputService.Enable(true);
                return;
            }
                

            //if this is the placeholder game, setup a valid one and return
            if (gridModel.IsPlaceholder) 
            {
                SetupGame(new IntVector2(cell.X, cell.Y));
                inputService.Enable(true);
                return;
            }

            if (winCondition.IsLose(cell))
            {
                await GameEnd(false);
                inputService.Enable(true);
                return;
            }
            
            await OpenCells(cell);

            if (winCondition.IsWin(gridModel))
                await GameEnd(true);

            inputService.Enable(true);
        }

        private async Task OpenCells(ICell cell)
        {
            Flip(cell);

            var neighbors = GetNeighbors(cell);
            while (neighbors.Count != 0)
            {
                var processingNeighbors = neighbors.ToList(); //make a copy to avoid modifying list while iterating
                foreach (var neighbor in neighbors)
                {
                    if (neighbor is CellEmpty cellEmpty && !cellEmpty.IsFlipped)
                    {
                        Flip(cellEmpty);

                        if (cellEmpty.TouchingBombs > 0) //remove the slots that are touching a bomb
                            processingNeighbors.Remove(cellEmpty);
                    }
                }

                neighbors = processingNeighbors.SelectMany(n => GetNeighbors(n)).Distinct().ToList();
                await Task.Delay(50);
            }

            List<ICell> GetNeighbors(ICell targetCell) => gridModel.Grid
                .GetNeighbors(targetCell.X, targetCell.Y)
                .Where(n => n != null && n is CellEmpty && !n.IsFlipped && !n.IsMarked)
                .ToList();
        }

        //here I'm encapsulating the rule of updating UI and Model to avoid duplications
        private void Mark(ICell cell)
        {
            if (cell.IsFlipped)
                return;

            var isMarked = !cell.IsMarked;
            cell.SetMarked(isMarked);
            gameView.SetMarked(cell, isMarked);
        }

        //here I'm encapsulating the rule of updating UI and Model to avoid duplications
        private void Flip(ICell cell)
        {
            cell.SetFlipped();
            gameView.SetFlipped(cell);
        }
    }
}