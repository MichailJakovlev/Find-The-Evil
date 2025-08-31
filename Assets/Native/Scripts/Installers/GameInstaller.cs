using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private EventBus _eventBus;
    [SerializeField] private GameState _gameState;
    [SerializeField] private SceneState _sceneState;
    [SerializeField] private AudioState _audioState;
    [SerializeField] private Authorization _authorization;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private RewardAd _rewardAd;
    [SerializeField] private FullscreenAd _fullscreenAd;
    [SerializeField] private RateGame _rateGame;
    [SerializeField] private ReadyGameAPI _readyGameAPI;
    [SerializeField] private Health _health;
    [SerializeField] private RoundDirector _roundDirector;
    [SerializeField] private UISwitcher _uiSwitcher;

    public override void InstallBindings()
    {
        EventBus eventBus = Container.InstantiatePrefabForComponent<EventBus>(_eventBus, _eventBus.transform.position, Quaternion.identity, null);
        Container.Bind<IEventBus>().To<EventBus>().FromInstance(eventBus).AsSingle();

        GameState gameState = Container.InstantiatePrefabForComponent<GameState>(_gameState, _gameState.transform.position, Quaternion.identity, null);
        Container.Bind<IGameState>().To<GameState>().FromInstance(gameState).AsSingle();

        SceneState sceneState = Container.InstantiatePrefabForComponent<SceneState>(_sceneState, _sceneState.transform.position, Quaternion.identity, null);
        Container.Bind<ISceneState>().To<SceneState>().FromInstance(sceneState).AsSingle();

        AudioState audioState = Container.InstantiatePrefabForComponent<AudioState>(_audioState, _audioState.transform.position, Quaternion.identity, null);
        Container.Bind<IAudioState>().To<AudioState>().FromInstance(audioState).AsSingle();

        Authorization authorization = Container.InstantiatePrefabForComponent<Authorization>(_authorization, _authorization.transform.position, Quaternion.identity, null);
        Container.Bind<IAuthorization>().To<Authorization>().FromInstance(authorization).AsSingle();

        Leaderboard leaderboard = Container.InstantiatePrefabForComponent<Leaderboard>(_leaderboard, _leaderboard.transform.position, Quaternion.identity, null);
        Container.Bind<ILeaderboard>().To<Leaderboard>().FromInstance(leaderboard).AsSingle();

        RewardAd rewardAd = Container.InstantiatePrefabForComponent<RewardAd>(_rewardAd, _rewardAd.transform.position, Quaternion.identity, null);
        Container.Bind<IRewardAd>().To<RewardAd>().FromInstance(rewardAd).AsSingle();

        FullscreenAd fullsceenAd = Container.InstantiatePrefabForComponent<FullscreenAd>(_fullscreenAd, _fullscreenAd.transform.position, Quaternion.identity, null);
        Container.Bind<IFullscreenAd>().To<FullscreenAd>().FromInstance(fullsceenAd).AsSingle();

        RateGame rateGame = Container.InstantiatePrefabForComponent<RateGame>(_rateGame, _rateGame.transform.position, Quaternion.identity, null);
        Container.Bind<IRateGame>().To<RateGame>().FromInstance(rateGame).AsSingle();

        ReadyGameAPI readyGameAPI = Container.InstantiatePrefabForComponent<ReadyGameAPI>(_readyGameAPI, _readyGameAPI.transform.position, Quaternion.identity, null);
        Container.Bind<IReadyGameAPI>().To<ReadyGameAPI>().FromInstance(readyGameAPI).AsSingle();

        RoundDirector roundDirector = Container.InstantiatePrefabForComponent<RoundDirector>(_roundDirector, _roundDirector.transform.position, Quaternion.identity, null);
        
        Health health = Container.InstantiatePrefabForComponent<Health>(_health, _health.transform.position, Quaternion.identity, null);
        
        UISwitcher uiSwitcher = Container.InstantiatePrefabForComponent<UISwitcher>(_uiSwitcher, _uiSwitcher.transform.position, Quaternion.identity, null);
    }
}

