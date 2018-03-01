using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();
        Container.Bind<IPlayerCollisions>().To<PlayerCollisions>().AsSingle();
        Container.Bind<IPlayerSpriteRenderer>().To<PlayerSpriteRenderer>().AsSingle();
        Container.Bind<IPlayerFogOfWar>().To<PlayerFogOfWar>().AsSingle();

        Container.Bind<IFogOfWar>().To<FogOfWar>().AsSingle();
        Container.Bind<ITurn>().To<Turn>().AsSingle();
        Container.Bind<IReposition>().To<Reposition>().AsSingle();
    }
}