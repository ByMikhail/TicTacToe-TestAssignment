using TicTacToe.Core.GameModel;
using TicTacToe.Core.Infrastructure.SceneManagement;
using Zenject;

namespace TicTacToe.Core.CRL.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TicTacToeGame>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<SceneManager>()
                .AsSingle();

            Container
                .Bind<GameController>()
                .AsSingle();
        }
    }
}