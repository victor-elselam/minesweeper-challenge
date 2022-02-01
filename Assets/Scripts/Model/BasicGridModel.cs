namespace Assets.Scripts.Model
{
    public class BasicGridModel
    {
        public ICell[,] Grid;
        public int BombsCount { get; private set; }
        public virtual bool IsPlaceholder => true; //return if this is a placeholder grid

        public BasicGridModel(GridSettings gridSettings, int bombsCount)
        {
            Grid = GenerateGrid(gridSettings.Horizontal, gridSettings.Vertical);
            BombsCount = bombsCount;
        }

        private ICell[,] GenerateGrid(int horizontal, int vertical)
        {
            var grid = new ICell[horizontal, vertical];

            for (var i = 0; i < horizontal; i++)
            {
                for (var j = 0; j < vertical; j++)
                {
                    grid[i, j] = new CellEmpty(i, j);
                }
            }

            return grid;
        }
    }
}