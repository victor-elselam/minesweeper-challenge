using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MinesweeperUI : MonoBehaviour
    {
        [SerializeField] private Text bombsCountText;
        [SerializeField] private GameObject winObject;
        [SerializeField] private GameObject loseObject;

        public void Start()
        {
            winObject.SetActive(false);
            loseObject.SetActive(false);
        }

        public void GameEnd(bool win)
        {
            winObject.SetActive(win);
            loseObject.SetActive(!win);
        }

        public void SetupUI(int bombsCount)
        {
            bombsCountText.text = $"Bombs Count: {bombsCount}";
            winObject.SetActive(false);
            loseObject.SetActive(false);
        }
    }
}