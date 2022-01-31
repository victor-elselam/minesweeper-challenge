namespace Assets.Scripts.Model
{
    public class BasicGridModel
    {
        public ICell[,] Grid;
        public virtual bool IsPlaceholder => true; //return if this is a placeholder grid
        public int FlippedSlots
        {
            get
            {
                var count = 0;
                foreach (var cell in Grid)
                {
                    if (cell.IsFlipped)
                        count++;
                }

                return count;
            }
        }

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
                    grid[i, j] = new CellEmpty(i, j);
                }
            }

            return grid;
        }
    }
}