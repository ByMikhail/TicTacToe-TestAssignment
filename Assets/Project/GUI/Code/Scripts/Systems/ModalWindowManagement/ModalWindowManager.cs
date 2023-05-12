using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TicTacToe.GUI.Systems.ModalWindowManagement
{
    public class ModalWindowManager : MonoBehaviour
    {
        private Transform _container;
        private ModalWindowFactory _factory;
        private Stack<ModalWindow> _windows;

        [Inject]
        private void InjectDependencies(Transform container, ModalWindowFactory modalWindowFactory)
        {
            _container = container;
            _factory = modalWindowFactory;
        }

        #region Unity lifecycle

        private void Awake()
        {
            _windows = new Stack<ModalWindow>();
            _container.gameObject.SetActive(false);
        }

        #endregion

        public void Show(string title, string message, Action onClose = null)
        {
            if (_windows.Count > 0)
            {
                _windows.Peek().Hide();
            }
            else
            {
                _container.gameObject.SetActive(true);
            }

            var window = _factory.Create();

            window.Init(title, message);
            window.transform.SetParent(_container, false);
            window.OnClose += ModalWindow_OnClose;

            if (onClose != null)
            {
                window.OnClose += _ => onClose.Invoke();
            }

            _windows.Push(window);
        }

        private void ModalWindow_OnClose(ModalWindow modalWindow)
        {
            modalWindow.OnClose -= ModalWindow_OnClose;
            Destroy(modalWindow.gameObject);

            _windows.Pop();

            if (_windows.Count > 0)
            {
                _windows.Peek().Show();
            }
            else
            {
                _container.gameObject.SetActive(false);
            }
        }
    }
}