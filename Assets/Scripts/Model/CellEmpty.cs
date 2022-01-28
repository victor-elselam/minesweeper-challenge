namespace Assets.Scripts.Model
{
    public class CellEmpty : ICell
    {
        public int TouchingBombs;
        public bool IsMarked { get; set; }
    }
}