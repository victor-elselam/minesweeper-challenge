using Assets.Scripts.Model;
using Assets.Scripts.View;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellFactory
    {
        public static ICellView GetCell(ICell cellModel, GameSettings gameSettings, Transform parent)
        {
            if (cellModel is CellEmpty _)
                return Object.Instantiate(gameSettings.EmptyPrefab, parent);
            else if (cellModel is CellBomb _ )
                return Object.Instantiate(gameSettings.BombPrefab, parent);
            else
                throw new System.Exception($"No prefab for type: {cellModel.GetType()} found");
        }
    }
}