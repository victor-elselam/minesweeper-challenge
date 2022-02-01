using Assets.Scripts.Model;
using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GridView : MonoBehaviour
    {
        public event Action<ICell> OnMark;
        public event Action<ICell> OnFlip;

        private ICellView[,] cellViews;

        public void PopulateGrid(BasicGridModel gridModel, GameSettings gameSettings)
        {
            DestroyAndClear();

            var grid = gridModel.Grid;
            var horizontalLength = grid.GetLength(0);
            var verticalLength = grid.GetLength(1);

            cellViews = new ICellView[horizontalLength, verticalLength];

            Camera.main.orthographicSize = (float) (horizontalLength + verticalLength) / 10;
            transform.localPosition = new Vector3((float) horizontalLength * -0.15f, (float) verticalLength * -0.15f, 10);

            for (var i = 0; i < horizontalLength; i++)
            {
                for (var j = 0; j < verticalLength; j++)
                {
                    var cell = CellFactory.GetCell(grid[i, j], gameSettings, this.transform);
                    cell.SetModel(grid[i, j]);
                    SetupListeners(cell);
                    cellViews[i, j] = cell;
                }
            }
        }

        public void SetMarked(ICell cell, bool isMarked)
        {
            cellViews[cell.X, cell.Y].MarkView(isMarked);
        }

        public void SetFlipped(ICell cell)
        {
            cellViews[cell.X, cell.Y].FlipView();
        }

        public void DestroyAndClear()
        {
            if (cellViews == null || cellViews.Length == 0)
                return;

            foreach(var cellView in cellViews)
                cellView.Destroy();

            cellViews = null;
        }

        private void SetupListeners(ICellView cellView)
        {
            //grid is encapsulating all cells events to simplify the Presenter listener
            //we don't need to unsubscribe this events, because Unity deals with it for us, since they are UnityEvents
            cellView.OnMark.AddListener((cell) => OnMark?.Invoke(cell));
            cellView.OnFlip.AddListener((cell) => OnFlip?.Invoke(cell));
        }
    }
}