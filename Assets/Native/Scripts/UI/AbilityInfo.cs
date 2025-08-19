using TMPro;
using UnityEngine;
using Zenject;

public class AbilityInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInfo;
    [SerializeField] private GameObject _background;
    public EventBus _eventBus;

    [Inject]
    private void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus.EventBus;
    }
    
    void Start()
    {
        _background.SetActive(false);
        _textInfo.enabled = false;
    }
    public void ShowAbilityInfo(string text, Card card)
    {
        _eventBus.StartUsingAbilityEvent(card);
        _background.SetActive(true);
        _textInfo.enabled = true;
        _textInfo.text = text;
    }

    public void HideAbilityInfo()
    {
        _eventBus.StopUsingAbilityEvent();
        _background.SetActive(false);
        _textInfo.enabled = false;
    }
}
