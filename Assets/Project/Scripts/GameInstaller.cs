using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();
        Container.Bind<IPlayerCollisions>().To<PlayerCollisions>().AsSingle();
        Container.Bind<IPlayerSpriteRenderer>().To<PlayerSpriteRenderer>().AsSingle();
    }
}