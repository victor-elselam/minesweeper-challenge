using Assets.Scripts.View;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Minesweeper/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        //it's normally a good practice to only allow getters to this configs, so no one will be able to change it directly from code
        [Header("General Settings")]
        [SerializeField] private GridSettings gridSettings;
        public GridSettings GridSettings => gridSettings;

        [SerializeField] private int minBombs = 2;
        public int MinBombs => minBombs;

        [SerializeField] private int maxBombs = 10;
        public int MaxBombs => maxBombs;

        [Header("Prefabs")]
        [SerializeField] private GridView gridViewPrefab;
        public GridView GridViewPrefab => gridViewPrefab;

        [SerializeField] private CellBombView bombPrefab;
        public CellBombView BombPrefab => bombPrefab;

        [SerializeField] private CellEmptyView emptyPrefab;
        public CellEmptyView EmptyPrefab => emptyPrefab;
    }

    [Serializable]
    public class GridSettings
    {
        [Range(5, 50)] public int Horizontal = 6;
        [Range(5, 50)] public int Vertical = 6;
    }
}