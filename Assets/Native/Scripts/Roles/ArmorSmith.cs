using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmorSmith : Role
{
    private string message = "";
    public override string SendMessage()
    {
        if (_roleType == "Evil" || _isCorrupted)
        {
            return SayLie(_cardNumber);
        }
        else
        {
            return SayTruth();
        }
    }

    public override string SayTruth()
    {
        return message;
    }

    public override string SayLie(int evilCardNumber)
    {
        return $"";
    }

    public override void PassiveAbility()
    {
        if (_card._cardRole._roleType != "Evil" && !_isCorrupted)
        {
            var cardAmount = _roleDirector.cardPool._cardAmount;
        
            var villagerList = _roleDirector.villagersCards;
            var outcastList = _roleDirector.outcastsCards;
            var mergedLists = villagerList.Concat(outcastList).ToList();

            foreach (var card in mergedLists)
            {
                if (card._cardRole._cardName == "Knight") mergedLists.Remove(card);
            }

            var randomIndex = Random.Range(0, mergedLists.Count);
            var armoredCard = mergedLists[randomIndex];
            armoredCard._cardRole._cardHandler.isArmored = true;
            Debug.Log("I made armor for " + armoredCard._cardName.text + ": " + armoredCard._cardNumber.text);


            message = $"I made armor for {armoredCard._cardNumber.text}";
        }
    }
    
}
