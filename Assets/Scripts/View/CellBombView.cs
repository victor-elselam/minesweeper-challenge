using Assets.Scripts.Model;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class CellBombView : MonoBehaviour, ICellView
    {
        public ICell Model { get; private set; }

        public UnityEvent<ICell> OnMark => onMark;
        private UnityEvent<ICell> onMark = new UnityEvent<ICell>();
        public UnityEvent<ICell> OnFlip => onFlip;
        private UnityEvent<ICell> onFlip = new UnityEvent<ICell>();

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
            onFlip?.Invoke();
        }

        public void Mark()
        {
            mark.gameObject.SetActive(true);
            onMark.Invoke();
        }

        public void SetModel(ICell cell)
        {
            Model = cell;
        }

        public void SetPosition(int x, int y)
        {
            transform.position = new Vector3(x * 2, y * 2, 0);
        }
    }
}