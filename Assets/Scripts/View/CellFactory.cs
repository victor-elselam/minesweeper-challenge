using Assets.Scripts.Model;
using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellFactory
    {
        public static ICellView GetCell(ICell cellModel, GameSettings gameSettings)
        {
            if (cellModel is CellEmpty _)
                return Object.Instantiate(gameSettings.EmptyPrefab);
            else if (cellModel is CellBomb _ )
                return Object.Instantiate(gameSettings.BombPrefab);
            else
                throw new System.Exception($"No prefab for type: {cellModel.GetType()} found");
        }
    }
}