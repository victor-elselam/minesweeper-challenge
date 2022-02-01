using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Presenter
{
    public class MinesweeperPresenter
    {
        private BasicGridModel gridModel;
        private GameSettings gameSettings;
        private GridView gridView;
        private MonoBehaviour coroutineHost;
        private int bombsCount;

        public MinesweeperPresenter(GameSettings gameSettings, GridView gridView, MonoBehaviour coroutineHost)
        {
            this.gameSettings = gameSettings;
            this.gridView = gridView;
            this.coroutineHost = coroutineHost;
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
            bombsCount = UnityEngine.Random.Range(gameSettings.MinBombs, gameSettings.MaxBombs);
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
            //return to don't allow flipping a marked cell
            if (cell.IsMarked)
                return;

            if (gridModel.IsPlaceholder && gridModel.FlippedSlots == 0)
            {
                SetupGame(new IntVector2(cell.X, cell.Y));
                return;
            }

            if (cell is CellBomb)
            {
                gridView.GameEnd(false);
            }

            coroutineHost.StartCoroutine(OpenCells(cell, CheckForWin));
        }

        private void CheckForWin()
        {
            foreach(var cell in gridModel.Grid)
            {
                if (cell is CellEmpty && !cell.IsFlipped)
                    return;
            }
            gridView.GameEnd(true);
        }

        private IEnumerator OpenCells(ICell cell, Action onComplete)
        {
            cell.SetFlipped();
            var neighbors = GetNeighbors(cell);
            while (neighbors.Count != 0)
            {
                var processingNeighbors = neighbors.ToList(); //make a copy to avoid modifying list while iterating
                foreach (var neighbor in neighbors)
                {
                    if (neighbor is CellEmpty cellEmpty && !cellEmpty.IsFlipped)
                    {
                        Debug.Log($"Flipping: X: {neighbor.X} Y: {neighbor.Y}");
                        neighbor.SetFlipped();

                        if (cellEmpty.TouchingBombs > 0) //remove the slots that are touching a bomb
                            processingNeighbors.Remove(cellEmpty);
                    }
                }

                neighbors = processingNeighbors.SelectMany(n => GetNeighbors(n)).Distinct().ToList();
                yield return null;
            }

            onComplete?.Invoke();

            List<ICell> GetNeighbors(ICell targetCell) => gridModel.Grid
                .GetNeighbors(targetCell.X, targetCell.Y)
                .Where(n => n != null && n is CellEmpty cellEMpty && n != targetCell && !n.IsFlipped && !n.IsMarked)
                .ToList();
        }
    }
}