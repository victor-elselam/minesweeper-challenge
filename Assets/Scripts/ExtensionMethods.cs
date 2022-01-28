using Assets.Scripts.Model;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get neighbors from specified position.
        /// </summary>
        /// <param name="grid">grid</param>
        /// <param name="x">horizontal index</param>
        /// <param name="y">vertical index</param>
        /// <returns>
        /// 0 - up
        /// 1 - up right
        /// 2 - right
        /// 3 - down right
        /// 4 - down
        /// 5 - down left
        /// 6 - left
        /// 7 - up left
        /// <returns>Neightbors from specified position</returns>
        public static List<ICell> GetNeighbors(this ICell[,] grid, int x, int y)
        {
            return new List<ICell>
            {
                grid.SafeGetAt(x, y + 1), //up
                grid.SafeGetAt(x + 1, y + 1), //up right

                grid.SafeGetAt(x + 1, y), //right
                grid.SafeGetAt(x + 1, y - 1), //down right

                grid.SafeGetAt(x, y - 1), //down
                grid.SafeGetAt(x - 1 , y - 1), //down left

                grid.SafeGetAt(x - 1, y), //left
                grid.SafeGetAt(x - 1, y + 1), //up left
            };
        }

        /// <summary>
        /// Safe get in grid, returns null if the position is out of range
        /// </summary>
        /// <param name="grid">grid</param>
        /// <param name="x">horizontal index</param>
        /// <param name="y">vertical index</param>
        /// <returns></returns>
        public static ICell SafeGetAt(this ICell[,] grid, int x, int y)
        {
            try
            {
                return grid[x, y];
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}