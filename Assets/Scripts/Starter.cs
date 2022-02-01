using Assets.Scripts.Presenter;
using Assets.Scripts.Services;
using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts
{
    //the perfect solution would be a Dependency Injection, but maybe it would be an overkill for this situation,
    //a simple Starter can handle without much problems
    public class Starter : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        private InputService inputService;
        private IGameView view;
        private MinesweeperPresenter presenter;

        public void Start()
        {
            this.inputService = new InputService();
            this.view = Instantiate(gameSettings.GameViewPrefab);
            this.presenter = new MinesweeperPresenter(gameSettings, view);
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