using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public abstract class CellBaseView : MonoBehaviour, ICellView, IClickable
    {
        public ICell Model { get; private set; }

        public CellUnityEvent OnMark { get; } = new CellUnityEvent();
        public CellUnityEvent OnFlip { get; } = new CellUnityEvent();

        [SerializeField] private SpriteRenderer cover;
        [SerializeField] private SpriteRenderer mark;

        public virtual void Start()
        {
            cover.gameObject.SetActive(true);
            mark.gameObject.SetActive(false);
        }

        public void InputToFlip() => OnFlip?.Invoke(Model); //here the view warn the presenter that it has been cliked
        private void FlipView() => cover.gameObject.SetActive(false); //here the view respond to the model modification

        public void InputToMark() => OnMark?.Invoke(Model); //here the view warn the presenter that it has been cliked
        private void MarkView(bool isMarked) => mark.gameObject.SetActive(isMarked); //here the view respond to the model modification

        public virtual void SetModel(ICell cell)
        {
            Model = cell;
            SetPosition(cell.X, cell.Y);
            Model.OnMark += MarkView;
            Model.OnFlip += FlipView;
        }

        public void OnDestroy()
        {
            Model.OnMark -= MarkView;
            Model.OnFlip -= FlipView;
        }

        public virtual void SetPosition(int x, int y)
        {
            transform.localPosition = new Vector3(x * 0.3f, y * 0.3f, 0);
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}