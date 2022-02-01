using Assets.Scripts.Model;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class CellEmptyView : CellBaseView
    {
        [SerializeField] private TextMeshPro touchingBombsCount;

        public override void SetModel(ICell cell)
        {
            base.SetModel(cell);
            var touchingBombs = ((CellEmpty)cell).TouchingBombs;
            touchingBombsCount.text = touchingBombs > 0 ? touchingBombs.ToString() : touchingBombs.ToString();
        }
    }
}