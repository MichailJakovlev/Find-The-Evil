using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WitchDoctor : Role
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
        List<int> countCorruptedList = new List<int>() { 1, 2 };
        
        var randomIndex = Random.Range(0, countCorruptedList.Count);
        return $"I cured {countCorruptedList[randomIndex]} Corruptions";
    }

    public override void PassiveAbility()
    {
        if (_card._cardRole._roleType != "Evil" && !_isCorrupted)
        {
            var cardAmount = _roleDirector.cardPool._cardAmount;
        
            var villagerList = _roleDirector.villagersCards;
            var outcastList = _roleDirector.outcastsCards;
            var mergedLists = villagerList.Concat(outcastList).ToList();
        
            List<Card> corruptedList = new List<Card>();
        
            var corruptedCount = 0;
        
            int leftNeighbor = (_cardNumber - 2 + cardAmount) % cardAmount + 1;
            int rightNeighbor = _cardNumber % cardAmount + 1;

            foreach (var card in mergedLists)
            {
                if (card._cardRole._isCorrupted)
                {
                    corruptedList.Add(card);
                }
            }
            
            foreach (var card in corruptedList)
            {
                if (card.cardId == leftNeighbor)
                {
                    card._cardRole._isCorrupted = false;
                    corruptedCount++;
                }
                else if (card.cardId == rightNeighbor)
                {
                    card._cardRole._isCorrupted = false;
                    corruptedCount++;
                }
            }

            message = $"I cured {corruptedCount} Corruptions";
        }
    }
    
}
