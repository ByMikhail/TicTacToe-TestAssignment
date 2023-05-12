using UnityEngine;

namespace TicTacToe.GUI.Configs
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Config/UI/MainConfig")]
    internal class MainConfig : ScriptableObject
    {
        [SerializeField] private Color _playerColorX;
        [SerializeField] private Color _playerColorO;

        public Color PlayerColorX => _playerColorX;
        public Color PlayerColorO => _playerColorO;
    }
}