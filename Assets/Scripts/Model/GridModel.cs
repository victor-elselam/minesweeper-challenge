using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class GridModel : BasicGridModel
    {
        public override bool IsPlaceholder => false;

        public GridModel(GridSettings gridSettings, int bombsCount, IntVector2 firstSquare) : base(gridSettings)
        {
            Grid = GenerateGrid(gridSettings.Horizontal, gridSettings.Vertical, bombsCount, firstSquare);
        }

        private ICell[,] GenerateGrid(int horizontal, int vertical, int bombsCount, IntVector2 firstSquare)
        {
            var grid = new ICell[horizontal, vertical];
            var bombPositions = GetBombPositions(horizontal, vertical, bombsCount, firstSquare);

            for (var i = 0; i < bombPositions.Count; i++)
            {
                grid[bombPositions[i].X, bombPositions[i].Y] = new CellBomb(bombPositions[i].X, bombPositions[i].Y);
            }

            for (var i = 0; i < horizontal; i++)
            {
                for (var j = 0; j < vertical; j++)
                {
                    if (grid[i, j] == null)
                    {
                        var cell = new CellEmpty(i, j);
                        cell.TouchingBombs = grid.GetNeighbors(i, j).Where(n => n != null && n is CellBomb).Count();
                    }
                }
            }

            return grid;
        }

        private List<IntVector2> GetBombPositions(int horizontal, int vertical, int bombsCount, IntVector2 firstSquare)
        {
            var bombPositions = new List<IntVector2>();
            for (var i = 0; i < bombsCount; i++)
            {
                var pos = GeneratePos();
                while (pos.X == firstSquare.X && pos.Y == firstSquare.Y)
                    pos = GeneratePos();

                bombPositions.Add(new IntVector2(pos.X, pos.Y));

                IntVector2 GeneratePos() => new IntVector2(Random.Range(0, horizontal), Random.Range(0, vertical));
            }

            return bombPositions;
        }
    }
}