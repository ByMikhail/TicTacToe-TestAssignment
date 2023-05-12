using System;
using UnityEngine;

namespace TicTacToe.GUI.Views.Grid
{
    internal interface IGridCellView
    {
        public Vector2Int Position { get; }
        public RectTransform ContentHolder { get; }
        public bool IsFilled { get; set; }
        public bool Interactable { get; set; }
        public event Action<IGridCellView> OnClick;
    }
}