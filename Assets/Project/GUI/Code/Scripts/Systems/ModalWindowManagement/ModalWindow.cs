using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.Systems.ModalWindowManagement
{
    [DisallowMultipleComponent]
    public class ModalWindow : MonoBehaviour
    {
        public event Action<ModalWindow> OnClose;

        private TMP_Text _titleText;
        private TMP_Text _messageText;
        private Button _closeButton;

        [Inject]
        private void InjectDependencies
        (
            [Inject(Id = "TitleText")] TMP_Text titleText,
            [Inject(Id = "MessageText")] TMP_Text messageText,
            Button closeButton
        )
        {
            _titleText = titleText;
            _messageText = messageText;
            _closeButton = closeButton;
        }

        public void Init(string title, string message)
        {
            _titleText.text = title;
            _messageText.text = message;
        }

        #region Unity lifecycle

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseButton_OnClick);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseButton_OnClick);
        }

        #endregion

        internal void Show()
        {
            gameObject.SetActive(true);
        }

        internal void Hide()
        {
            gameObject.SetActive(false);
            OnClose?.Invoke(this);
        }

        private void CloseButton_OnClick()
        {
            OnClose?.Invoke(this);
        }
    }
}