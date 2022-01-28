namespace Assets.Scripts.Model
{
    public class BasicGridModel
    {
        public ICell[,] Grid;

        public BasicGridModel(GridSettings gridSettings)
        {
            Grid = GenerateGrid(gridSettings.Horizontal, gridSettings.Vertical);
        }

        private ICell[,] GenerateGrid(int horizontal, int vertical)
        {
            var grid = new ICell[horizontal, vertical];

            for (var i = 0; i < horizontal; i++)
            {
                for (var j = 0; j < vertical; j++)
                {
                    grid[i, j] = new CellEmpty();
                }
            }

            return grid;
        }
    }
}