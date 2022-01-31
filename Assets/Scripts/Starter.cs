using Assets.Scripts.Presenter;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        private InputService inputService;
        private MinesweeperPresenter presenter;

        public void Start()
        {
            this.inputService = new InputService();
            this.presenter = new MinesweeperPresenter(gameSettings);
        }

        public void Update()
        {
            if (inputService != null)
            {
                inputService.Update();
            }
        }
    }
}