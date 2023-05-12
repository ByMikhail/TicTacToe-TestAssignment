using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.Views
{
    internal class MarkView : MonoBehaviour
    {
        public Color Color
        {
            get => _markImage.color;
            set => _markImage.color = value;
        }

        private Image _markImage;

        [Inject]
        private void InjectDependencies(Image markImage)
        {
            _markImage = markImage;
        }
    }
}