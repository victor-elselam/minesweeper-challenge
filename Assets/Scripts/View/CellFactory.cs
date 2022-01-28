using Assets.Scripts.Model;
using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellFactory
    {
        public static ICellView GetCell(ICell cellModel, GameSettings gameSettings)
        {
            switch (cellModel)
            {
                case CellEmpty:
                    return Object.Instantiate(gameSettings.EmptyPrefab);
                case CellBomb:
                    return Object.Instantiate(gameSettings.BombPrefab);
                default:
                    throw new System.Exception($"No prefab for type: {cellModel.GetType()} found");
            }
        }
    }
}