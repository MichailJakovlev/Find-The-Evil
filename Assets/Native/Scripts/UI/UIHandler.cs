using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class UIHandler : MonoBehaviour
{

    private ILeaderboard _leaderboard;
    private ISceneState _sceneState;
    private IFullscreenAd _fullscreenAd;
    private IRewardAd _rewardAd;
    private IAuthorization _authorization;
    private IEventBus _eventBus;
    private IRateGame _rateGame;
    private IReadyGameAPI _readyGameAPI;

    public bool _isAuthed = false;
    public bool _isKillMode = false;

    [Inject]
    private void Cunstruct(ILeaderboard uileaderbord, ISceneState sceneState, IFullscreenAd fullscreenAd, IRewardAd rewardAd, IAuthorization authorization, IEventBus eventBus, IRateGame rateGame, IReadyGameAPI readyGameAPI)
    {
        _leaderboard = uileaderbord;
        _sceneState = sceneState;
        _fullscreenAd = fullscreenAd;
        _rewardAd = rewardAd;
        _authorization = authorization;
        _eventBus = eventBus;
        _rateGame = rateGame;
        _readyGameAPI = readyGameAPI;
    }

    private void OnEnable()
    {
        EventBus.AuthPlayer += AuthCheck;
    }

    private void OnDisable()
    {
        EventBus.AuthPlayer -= AuthCheck;
    }

    void AuthCheck(int authStatus)
    {
        if (authStatus == 1)
        {
            _isAuthed = true;
        }
    }

    public void ToGameScene() => _sceneState.SceneState.ToGameScene();
    public void ToMenuScene() => _sceneState.SceneState?.ToMenuScene();
    public void ShowFullscreenAd() => _fullscreenAd.FullscreenAd.ShowFullscreenAd();
    public void ShowRewardAd(int num) => _rewardAd.RewardAd.ShowRewardAd(num);
    public void Auth() => _authorization.Authorization.Auth();
    public void StopGame() => _eventBus.EventBus.StopGameEvent();
    public void StartGame() => _eventBus?.EventBus.StartGameEvent();
    
    public void KillModeOn() => _eventBus?.EventBus.KillModeOnEvent();
    public void KillModeOff() => _eventBus?.EventBus.KillModeOffEvent();

    public void KillMode()
    {
        if (_isKillMode)
        {
            KillModeOff();
            _isKillMode = false;
        }
        else
        {
            KillModeOn();
            _isKillMode = true;
        }
        
    }

    public void OpenLeaderboard()
    {
        if (_isAuthed == true)
        {
            _leaderboard.Leaderboard.OpenLeaderboard();
        }
        else
        {
            _authorization.GameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OpenRateGamePage()
    {
        if (_isAuthed == true)
        {
            _rateGame.RateGame.OpenRateGamePage();
        }
        else
        {
            _authorization.GameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
