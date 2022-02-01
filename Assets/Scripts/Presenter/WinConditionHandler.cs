using Assets.Scripts.Model;

namespace Assets.Scripts.Presenter
{
    public class WinConditionHandler
    {
        public bool IsWin(BasicGridModel gridModel)
        {
            foreach (var cell in gridModel.Grid)
            {
                if (cell is CellEmpty && !cell.IsFlipped)
                    return false;
            }
            return true;
        }

        public bool IsLose(ICell cell)
        {
            return cell is CellBomb;
        }
    }
}