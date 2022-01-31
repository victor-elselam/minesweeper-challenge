using Assets.Scripts.Model;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class CellEmptyView : MonoBehaviour, ICellView, IClickable
    {
        public ICell Model { get; private set; }

        public CellUnityEvent OnMark { get; } = new CellUnityEvent();
        public CellUnityEvent OnFlip { get; } = new CellUnityEvent();

        [SerializeField] private Text touchingBombsCount;
        [SerializeField] private Image cover;
        [SerializeField] private Image mark;

        public void Start()
        {
            cover.gameObject.SetActive(true);
            mark.gameObject.SetActive(false);
        }

        public void SetModel(ICell cell)
        {
            Model = cell;
            SetPosition(cell.X, cell.Y);
            var touchingBombs = ((CellEmpty)cell).TouchingBombs;
            touchingBombsCount.text = touchingBombs > 0 ? touchingBombs.ToString() : string.Empty;
            Model.OnMark += MarkView;
            Model.OnFlip += FlipView;
        }

        public void InputToFlip() => OnFlip?.Invoke(Model); //here the view warn the presenter that it has been cliked
        private void FlipView() => cover.gameObject.SetActive(false); //here the view respond to the model modification

        public void InputToMark() => OnMark?.Invoke(Model); //here the view warn the presenter that it has been cliked
        private void MarkView(bool isMarked) => mark.gameObject.SetActive(isMarked); //here the view respond to the model modification

        public void SetPosition(int x, int y)
        {
            transform.position = new Vector3(x * 2, y * 2, 0);
        }

        public void Destroy() => throw new NotImplementedException();
    }
}