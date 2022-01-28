using Assets.Scripts.Model;
using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GridView : MonoBehaviour
    {
        public event Action 

        public void PopulateGrid(BasicGridModel gridModel, GameSettings gameSettings)
        {
            var grid = gridModel.Grid;
            var horizontalLength = grid.GetLength(0);
            var verticalLength = grid.GetLength(1);

            Camera.main.transform.position = new Vector3(horizontalLength - 1, verticalLength, -10);

            for (var i = 0; i < horizontalLength; i++)
            {
                for (var j = 0; j < verticalLength; j++)
                {
                    var cell = CellFactory.GetCell(grid[i, j], gameSettings);
                    cell.SetPosition(x: i, y: j);
                    cell.SetModel(grid[i, j]);
                }
            }
        }

        private void SetupListeners(ICellView cellView)
        {
            cellView.OnMark.AddListener
        }


    }
}