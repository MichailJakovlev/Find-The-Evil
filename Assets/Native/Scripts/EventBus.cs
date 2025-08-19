using System;
using UnityEngine;

public class EventBus : MonoBehaviour , IEventBus
{
    private IEventBus _eventBus;
    EventBus IEventBus.EventBus => this;

    public static event Action StartGame;
    public static event Action StopGame;
    public static event Action<int> AuthPlayer;
    public static event Action KillModeOn;
    public static event Action KillModeOff;
    public static event Action RestartRound;
    public static event Action<Card> StartUsingAbility;
    public static event Action StopUsingAbility;
    
    public void StartGameEvent() => StartGame?.Invoke();
    public void StopGameEvent() => StopGame?.Invoke();
    public void AuthPlayerEvent(int authPlayerStatus) => AuthPlayer?.Invoke(authPlayerStatus);
    public void KillModeOnEvent() => KillModeOn?.Invoke();
    public void KillModeOffEvent() => KillModeOff?.Invoke();
    public void RestartRoundEvent() => RestartRound?.Invoke();
    public void StartUsingAbilityEvent(Card usedCard) => StartUsingAbility?.Invoke(usedCard);
    public void StopUsingAbilityEvent() => StopUsingAbility?.Invoke();
}
