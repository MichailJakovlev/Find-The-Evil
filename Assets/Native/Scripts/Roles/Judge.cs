using UnityEngine;

public class Judge : Role
{ 
    public string SayAbilityTruth(Card card)
    {
        if (card._cardRole._roleType == "Evil" || _cardStatus == "Corrupted")
        {
            return card._cardRole._cardNumber + " is Lying";
        }
        else
        {
            return card._cardRole._cardNumber + " said Truth";
        }
    }

    public string SayAbilityLie(Card card)
    {
        if (card._cardRole._roleType == "Evil" || _cardStatus == "Corrupted")
        {
            return card._cardRole._cardNumber + " said Truth";
        }
        else
        {
            return card._cardRole._cardNumber + " is Lying";
        }
    }

    public override void Ability(params Card[] selectedCards)
    {
        _card._cardMessage.gameObject.SetActive(true);
        if (_roleType == "Evil" || _cardStatus == "Corrupted")
        {
            _card._cardMessageText.text = SayAbilityLie(selectedCards[0]);
        }
        else
        {
           _card._cardMessageText.text = SayAbilityTruth(selectedCards[0]);
        }
        gameObject.GetComponentInParent<CardHandler>().isAbilityUsed = true;
        _abilityInfo.HideAbilityInfo();
    }
}
