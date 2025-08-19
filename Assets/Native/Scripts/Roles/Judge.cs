using UnityEngine;

public class Judge : Role
{ 
    public override void UseAbility()
    {
        _abilityInfo.ShowAbilityInfo("Pick 1 card", gameObject.GetComponentInParent<Card>());
    }

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

    public override void Ability(Card card)
    {
        gameObject.GetComponentInParent<Card>()._cardMessage.gameObject.SetActive(true);
        if (_roleType == "Evil" || _cardStatus == "Corrupted")
        {
            gameObject.GetComponentInParent<Card>()._cardMessageText.text = SayAbilityLie(card);
        }
        else
        {
            gameObject.GetComponentInParent<Card>()._cardMessageText.text = SayAbilityTruth(card);
        }
        gameObject.GetComponentInParent<CardHandler>().isAbilityUsed = true;
        _abilityInfo.HideAbilityInfo();
    }
}
