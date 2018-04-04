using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject level;
    public GameSceneManager gameSceneManager;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();
        Container.Bind<IPlayerCollisions>().To<PlayerCollisions>().AsSingle();
        Container.Bind<IPlayerSpriteRenderer>().To<PlayerSpriteRenderer>().AsSingle();
        Container.Bind<IPlayerFogOfWar>().To<PlayerFogOfWar>().AsSingle();

        Container.Bind<IFogOfWar>().To<FogOfWar>().AsSingle();
        Container.Bind<ITurn>().To<Turn>().AsSingle();
        Container.Bind<IReposition>().To<Reposition>().AsSingle();
        Container.Bind<IGameMap>().To<GameMap>().AsSingle();
        Container.Bind<IMouse>().To<Mouse>().AsSingle();
        Container.Bind<IAbility>().To<Ability>().AsSingle();
        Container.Bind<IMouseAttack>().To<MouseAttack>().AsSingle();
        Container.Bind<ISceneTransition>().To<SceneTransition>().AsSingle();

        Container.Bind<IOnPlayer>().To<OnPlayer>().AsSingle();

        Container.Bind<IPlayerMoveState>().To<PlayerMoveState>().AsSingle();
        Container.Bind<IPlayerWaitingTurnState>().To<PlayerWaitingTurnState>().AsSingle();
        Container.Bind<IPlayerAttackState>().To<PlayerAttackState>().AsSingle();

        Container.Bind<Reposition>().AsSingle();
        Container.Bind<FogOfWar>().AsSingle();
        Container.Bind<Turn>().AsSingle();
        Container.Bind<OnPlayer>().AsSingle();
        Container.Bind<PlayerMovement>().AsSingle();

        Container.BindInstance(level);
        Container.BindInstance(gameSceneManager);
    }
}