using Assets.Scripts.Model;
using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GridView : MonoBehaviour
    {
        public event Action<ICell> OnMark;
        public event Action<ICell> OnFlip;

        private List<ICellView> cellViews = new List<ICellView>();

        public void PopulateGrid(BasicGridModel gridModel, GameSettings gameSettings)
        {
            DestroyAndClear();

            var grid = gridModel.Grid;
            var horizontalLength = grid.GetLength(0);
            var verticalLength = grid.GetLength(1);

            Camera.main.transform.position = new Vector3(horizontalLength - 1, verticalLength, -10);

            for (var i = 0; i < horizontalLength; i++)
            {
                for (var j = 0; j < verticalLength; j++)
                {
                    var cell = CellFactory.GetCell(grid[i, j], gameSettings);
                    cell.SetModel(grid[i, j]);
                    SetupListeners(cell);
                    cellViews.Add(cell);
                }
            }
        }

        public void DestroyAndClear()
        {
            foreach(var cellView in cellViews)
                cellView.Destroy();

            cellViews.Clear();
        }

        private void SetupListeners(ICellView cellView)
        {
            //grid is encapsulating all cells events to simplify the Presenter listener
            cellView.OnMark.AddListener((cell) => OnMark?.Invoke(cell));
            cellView.OnFlip.AddListener((cell) => OnFlip?.Invoke(cell));
        }
    }
}