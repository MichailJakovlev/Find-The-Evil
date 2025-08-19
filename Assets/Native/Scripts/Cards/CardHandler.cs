using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardFlipAnimation cardFlipAnimation;
    [SerializeField] private Card card;
    private bool isUsingAbility = false;
    private Card usedAbilityCard;
    
    public bool isKillMode = false;
    public bool isAbilityUsed = false;
    public bool isFlipped = false;

    public void OnEnable()
    {
        EventBus.KillModeOn += CardKillModeOn;
        EventBus.KillModeOff += CardKillModeOff;
        EventBus.RestartRound += CloseCards;
        EventBus.StartUsingAbility += StartUsingAbility;
        EventBus.StopUsingAbility += StopUsingAbility;
    }

    public void OnDisable() 
    {
        EventBus.KillModeOn -= CardKillModeOn;
        EventBus.KillModeOff -= CardKillModeOff;
        EventBus.RestartRound -= CloseCards;
        EventBus.StartUsingAbility -= StartUsingAbility;
        EventBus.StopUsingAbility -= StopUsingAbility;
    }

    private void StartUsingAbility(Card card)
    {
        isUsingAbility = true;
        usedAbilityCard = card;
    }

    private void StopUsingAbility()
    {
        isUsingAbility = false;
    }
    
    public void CardKillModeOn()
    {
        isKillMode = true;
    }

    public void CardKillModeOff()
    {
        isKillMode = false;
    }
    
    private void Start()
    {
        card._cardName.gameObject.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardFlipAnimation.OnCardEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardFlipAnimation.OnCardExit();
    }
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (isFlipped && !isKillMode && !isUsingAbility && !isAbilityUsed)
            {
                if (card._cardRole._canUseAbility || card._cardRole._substituteRole._canUseAbility)
                {
                    if (card._cardRole._roleType == "Evil")
                    {
                        card._cardRole._substituteRole.UseAbility();
                    }
                    card._cardRole.UseAbility();
                }
            }

            else if(isFlipped && !isKillMode && isUsingAbility && !isAbilityUsed)
            {
                if (card._cardRole._roleType == "Evil")
                {
                    usedAbilityCard._cardRole._substituteRole.Ability(card);
                }
                else
                {
                    usedAbilityCard._cardRole.Ability(card);
                }
            }

            if (!isFlipped)
            {
                card.ShowMessage();
                card._cardName.gameObject.SetActive(true);
            }
            
            if (isKillMode)
            {
                card.KillCard();
            }
            
            cardFlipAnimation.OnCardClick();
        }
    }

    public void CloseCards()
    {
        cardFlipAnimation.OnCardClose();
        isAbilityUsed = false;
    }
}
