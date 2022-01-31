using Assets.Scripts.Model;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Presenter
{
    public class MinesweeperPresenter
    {
        private BasicGridModel gridModel;
        private GameSettings gameSettings;
        private GridView gridView;

        public MinesweeperPresenter(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings;
            SetupPlaceholderGame();
        }

        public void SetupPlaceholderGame()
        {
            var gridSettings = gameSettings.GridSettings;
            gridModel = new BasicGridModel(gridSettings);
            Setup(gridModel);
        }

        public void SetupGame(IntVector2 firstFlip)
        {
            var gridSettings = gameSettings.GridSettings;
            var bombsCount = Random.Range(gameSettings.MinBombs, gameSettings.MaxBombs);
            gridModel = new GridModel(gridSettings, bombsCount, firstFlip);
            Setup(gridModel);

            FlipCell(gridModel.Grid[firstFlip.X, firstFlip.Y]);
        }

        private void Setup(BasicGridModel gridModel)
        {
            //unregister events to avoid leak
            if (gridView != null)
            {
                gridView.OnMark -= MarkCell;
                gridView.OnFlip -= FlipCell;
            }

            gridView.PopulateGrid(gridModel, gameSettings);
            gridView.OnMark += MarkCell;
            gridView.OnFlip += FlipCell;
        }

        private void MarkCell(ICell cell)
        {
            cell.SetMarked(!cell.IsMarked);
        }

        private void FlipCell(ICell cell)
        {
            if (gridModel.IsPlaceholder && gridModel.FlippedSlots == 0)
            {
                SetupGame(new IntVector2(cell.X, cell.Y));
                return;
            }

            cell.SetFlipped();
            OpenCells(cell);
        }

        private void OpenCells(ICell cell)
        {
            var neighbors = gridModel.Grid.GetNeighbors(cell.X, cell.Y).Where(n => !n.IsFlipped && !n.IsMarked);
            foreach (var neighbor in neighbors)
            {
                if (neighbor is CellEmpty cellEmpty)
                {
                    cell.SetFlipped();
                    if (cellEmpty.TouchingBombs == 0)
                    {
                        OpenCells(cell);
                    }
                }
                else
                {
                    Debug.LogError("Opening bomb, something went wrong here!");
                }
            }
        }
    }
}