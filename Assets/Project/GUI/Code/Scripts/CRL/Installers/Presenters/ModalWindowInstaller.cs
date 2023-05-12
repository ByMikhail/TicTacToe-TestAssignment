using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Presenters
{
    public class ModalWindowInstaller : MonoInstaller
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _closeButton;

        public override void InstallBindings()
        {
            Container
                .Bind<TMP_Text>()
                .WithId("TitleText")
                .FromInstance(_titleText);

            Container
                .Bind<TMP_Text>()
                .WithId("MessageText")
                .FromInstance(_messageText);

            Container
                .Bind<Button>()
                .FromInstance(_closeButton);
        }
    }
}