using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Presenter
{
    public class MinesweeperPresenter
    {
        private BasicGridModel gridModel;
        private GameSettings gameSettings;
        private GridView gridView;

        public MinesweeperPresenter(GameSettings gameSettings, GridView gridView)
        {
            this.gameSettings = gameSettings;
            this.gridView = gridView;
        }

        public void StartGame()
        {
            var gridSettings = gameSettings.GridSettings;
            var bombsCount = Random.Range(gameSettings.MinBombs, gameSettings.MaxBombs);
            gridModel = new BasicGridModel(gridSettings);
            gridView.PopulateGrid(gridModel, gameSettings);
        }
    }
}