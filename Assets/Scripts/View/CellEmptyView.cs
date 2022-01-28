using Assets.Scripts.Model;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class CellEmptyView : MonoBehaviour, ICellView
    {
        public ICell Model { get; private set; }
        public event Action OnMark;
        public event Action OnFlip;

        [SerializeField] private Text touchingBombsCount;
        [SerializeField] private Image cover;
        [SerializeField] private Image mark;

        public void Start()
        {
            cover.gameObject.SetActive(true);
            mark.gameObject.SetActive(false);
        }

        public void Flip()
        {
            cover.gameObject.SetActive(false);
            OnFlip?.Invoke();
        }

        public void Mark()
        {
            mark.gameObject.SetActive(true);
            OnMark?.Invoke();
        }

        public void SetModel(ICell cell)
        {
            Model = cell;
            var touchingBombs = ((CellEmpty)cell).TouchingBombs;
            touchingBombsCount.text = touchingBombs > 0 ? touchingBombs.ToString() : string.Empty;
        }

        public void SetPosition(int x, int y)
        {
            transform.position = new Vector3(x * 2, y * 2, 0);
        }
    }
}