using ModestTree;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardFlipAnimation cardFlipAnimation;
    [SerializeField] private Card card;
    
    private bool isUsingAbility = false;
    private Card usedAbilityCard;
    private Ability ability;
    private RoundDirector roundDirector;
    
    public bool isKillMode = false;
    public bool isAbilityUsed = false;
    public bool isFlipped = false;
    public string cardMessageText;
    
    public bool isFlippable = true;
    public bool isKilled = false;
    public bool isArmored = false;
    
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

    private void Start()
    {
        ability = FindObjectOfType<Ability>();
        roundDirector = FindObjectOfType<RoundDirector>();
        card._cardName.gameObject.SetActive(false);
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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardFlipAnimation.OnCardEnter();
        if (isFlipped && !isKillMode)
        {
            if (!card._cardRole._canShowMessage || card._cardRole._canUseAbility && !isAbilityUsed)
            {
                card._cardMessage.gameObject.SetActive(true);
                card._cardMessage._messageBackground.gameObject.SetActive(false);
                card._cardMessage._cardMessageText.gameObject.SetActive(false);
                card._cardDescription._descriptionBackground.gameObject.SetActive(true);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(true);
            }
            else if(card._cardRole._roleType == "Evil" && card._cardRole._substituteRole._canUseAbility && !isAbilityUsed)
            {
                card._cardMessage.gameObject.SetActive(true);
                card._cardMessage._messageBackground.gameObject.SetActive(false);
                card._cardMessage._cardMessageText.gameObject.SetActive(false);
                card._cardDescription._descriptionBackground.gameObject.SetActive(true);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(true);
            }
            else if(isUsingAbility && isAbilityUsed)
            {
                card._cardMessage.gameObject.SetActive(false);
                card._cardMessage._messageBackground.gameObject.SetActive(false);
                card._cardMessage._cardMessageText.gameObject.SetActive(false); 
                card._cardDescription._descriptionBackground.gameObject.SetActive(true);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(true);
            }
            else
            {
                card._cardMessage._messageBackground.gameObject.SetActive(false);
                card._cardMessage._cardMessageText.gameObject.SetActive(false);
                card._cardDescription._descriptionBackground.gameObject.SetActive(true);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardFlipAnimation.OnCardExit();
        if (isFlipped)
        {
            if (!card._cardRole._canShowMessage || card._cardRole._canUseAbility && !isAbilityUsed)
            {
                card._cardMessage.gameObject.SetActive(false);
                card._cardMessage._messageBackground.gameObject.SetActive(true);
                card._cardMessage._cardMessageText.gameObject.SetActive(true);
                card._cardDescription._descriptionBackground.gameObject.SetActive(false);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(false);
            }
            else if(card._cardRole._roleType == "Evil" && card._cardRole._substituteRole._canUseAbility && !isAbilityUsed)
            {
                card._cardMessage.gameObject.SetActive(false);
                card._cardMessage._messageBackground.gameObject.SetActive(true);
                card._cardMessage._cardMessageText.gameObject.SetActive(true);
                card._cardDescription._descriptionBackground.gameObject.SetActive(false);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(false);
            }
            else if(isUsingAbility && isAbilityUsed)
            {
                card._cardMessage.gameObject.SetActive(true);
                card._cardMessage._messageBackground.gameObject.SetActive(true);
                card._cardMessage._cardMessageText.gameObject.SetActive(true);
                card._cardDescription._descriptionBackground.gameObject.SetActive(false);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(false);
            }
            else
            {
                card._cardMessage._messageBackground.gameObject.SetActive(true);
                card._cardMessage._cardMessageText.gameObject.SetActive(true);
                card._cardDescription._descriptionBackground.gameObject.SetActive(false);
                card._cardDescription._cardDescriptionText.gameObject.SetActive(false);
            }
        }
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
                        ability.StartSelectCard(card._cardRole._substituteRole._card);
                    }
                    else
                    {
                        ability.StartSelectCard(card);
                    }
                }
            }

            else if(isFlipped && !isKillMode && isUsingAbility)
            {
                if (card._cardRole._roleType == "Evil")
                {
                    ability.SelectCard(card);
                }
                else
                {
                    ability.SelectCard(card);
                }
            }

            if (!isFlipped)
            {
                if (isFlippable)
                {
                    card._cardName.gameObject.SetActive(true);
                    card.ShowMessage();
                    roundDirector.openedCards++;

                    roundDirector.closedCardsList.Remove(card);
                    
                    // <Debug Logs>
                    if (roundDirector.cardsInRoundList.IsEmpty() && roundDirector.openedCards == roundDirector.cardsInRoundList.Count)
                    {
                        Debug.LogError("cardsInRoundList is Empty");
                    }
                    
                    string result = "List contents: ";
                    foreach (var num in roundDirector.closedCardsList)
                    {
                        result += $"[{num._cardRole._cardNumber}]" + num._cardRole._cardName.ToString() + ", ";
                    }
                    
                    Debug.Log($"role: " + card._cardRole._cardName + $", substituteRole: "
                              + card._cardRole._substituteRole._cardName + "\n" + result);
                    // </Debug Logs>
                    
                    if (card._cardRole._substituteRole._cardName == "Maniac")
                    {
                        Debug.Log(card._cardRole._substituteRole._cardNumber);
                        if (!roundDirector.closedCardsList.IsEmpty())
                        {
                            int randomIndex = 0;
                            randomIndex += Random.Range(0, roundDirector.closedCardsList.Count);
                            var killedCard = roundDirector.closedCardsList[randomIndex];
                            if (killedCard._cardRole._cardName != "Bomber")
                            {
                                roundDirector.closedCardsList[randomIndex].KillCardAbility();
                                if (killedCard._cardRole._substituteRole._cardName == "Knight" && killedCard._cardRole._isCorrupted == false) // ??????????
                                {
                                    card._health.Damage(0);
                                }
                                else if (killedCard._cardRole._roleType == "Evil")
                                {
                                    card._health.Damage(0);
                                }
                                else
                                {
                                    card._health.Damage(card._cardRole._substituteRole._abilityDamage);
                                }
                            }
                            else
                            {
                                Debug.Log("<b>I'm afraid of Bomber</b>");
                            } 
                        }
                    }
                    
                    cardFlipAnimation.OnCardClick();
                }
            }
            
            if (isKillMode)
            {
                isKilled = true;
                card.KillCard();
                roundDirector.cardsInRoundList.Remove(card);
                if (!isFlipped)
                {
                    card.ShowMessage();
                    roundDirector.openedCards++;
                    Debug.Log(card._cardRole._cardName);
                }
                card._cardName.gameObject.SetActive(true);
                cardFlipAnimation.OnCardClick();
            }
        }
    }
    
    public void CloseCards()
    {
        isFlippable = true;
        isKilled = false;
        isAbilityUsed = false;
        cardFlipAnimation.OnCardClose();
    }
}
