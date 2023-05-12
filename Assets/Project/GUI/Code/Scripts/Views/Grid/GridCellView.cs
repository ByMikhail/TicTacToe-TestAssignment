using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.Views.Grid
{
    [RequireComponent(typeof(Button))]
    internal class GridCellView : MonoBehaviour, IGridCellView
    {
        public RectTransform ContentHolder => _contentHolder;
        public Vector2Int Position => _position;

        public bool IsFilled
        {
            get => _fillImage.enabled;
            set => _fillImage.enabled = value;
        }

        public bool Interactable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        public event Action<IGridCellView> OnClick;

        private RectTransform _contentHolder;
        private Image _fillImage;
        private Button _button;
        private Vector2Int _position;

        [Inject]
        private void InjectDependencies(RectTransform contentHolder, Image fillImage)
        {
            _contentHolder = contentHolder;
            _fillImage = fillImage;
        }

        public void Init(Vector2Int position)
        {
            _position = position;
        }

        #region Unity lifecycle

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(Button_OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Button_OnClick);
        }

        #endregion

        #region Event handlers

        private void Button_OnClick()
        {
            OnClick?.Invoke(this);
        }

        #endregion
    }
}