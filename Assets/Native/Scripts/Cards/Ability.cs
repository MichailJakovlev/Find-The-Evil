using UnityEngine;
using Zenject;

public class Ability : MonoBehaviour
{
    [SerializeField] private AbilityInfo _abilityInfo;
    
    private Card _usedCard;
    private Card[] _selectedCards;
    private int _iterator = 0;
    private EventBus _eventBus;

    [Inject]
    private void Construct(IEventBus eventBus)
    {
        _eventBus = eventBus.EventBus;
    }
    
    public void StartSelectCard(Card usedCard)
    {
        _eventBus.StartUsingAbilityEvent(usedCard);
        _usedCard = usedCard;
        _selectedCards = new Card[_usedCard._cardRole._selectCardAmount];
        _abilityInfo.ShowAbilityInfo(AbilityText());
    }

    public string AbilityText()
    {
        if (_usedCard._cardRole._selectCardAmount == 1)
        {
            return "Pick " + _usedCard._cardRole._selectCardAmount + " card";
        }
        else
        {
            return "Pick " + _usedCard._cardRole._selectCardAmount + " cards";
        }
    }

    public void SelectCard(Card selectedCard)
    {
        if (_iterator < _selectedCards.Length)
        {
            _selectedCards[_iterator] = selectedCard;
            _iterator++;
        }

        if (_iterator == _selectedCards.Length)
        {
            if (_usedCard._cardRole._roleType == "Evil")
            {
                _usedCard._cardRole._substituteRole.Ability(_selectedCards);
            }
            else
            {
                _usedCard._cardRole.Ability(_selectedCards);
            }
            
            StopSelectCard();
        }
    }
    
    public void StopSelectCard()
    {
        _iterator = 0;
        _eventBus.StopUsingAbilityEvent();
        _abilityInfo.HideAbilityInfo();
    }
}
