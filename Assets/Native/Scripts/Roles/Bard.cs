using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bard : Role
{
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

        if (corruptedList.Any(corrupted => corrupted.cardId == leftNeighbor))
        {
            corruptedCount++;
        }
        if (corruptedList.Any(corrupted => corrupted.cardId == rightNeighbor))
        {
            corruptedCount++;
        }

        return $"{corruptedCount} Corrupted adjacent to me";
    }

    public override string SayLie(int evilCardNumber)
    {
        var cardAmount = _roleDirector.cardPool._cardAmount;
        
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        var mergedLists = villagerList.Concat(outcastList).ToList();
        
        List<Card> corruptedList = new List<Card>();
        List<int> countCorruptedList = new List<int>() { 0, 1, 2 };
        
        var corruptedCount = 0;
        
        int leftNeighbor = (evilCardNumber - 2 + cardAmount) % cardAmount + 1;
        int rightNeighbor = evilCardNumber % cardAmount + 1;

        foreach (var card in mergedLists)
        {
            if (card._cardRole._isCorrupted)
            {
                corruptedList.Add(card);
            }
        }

        if (corruptedList.Any(corrupted => corrupted.cardId == leftNeighbor))
        {
            corruptedCount++;
        }
        if (corruptedList.Any(corrupted => corrupted.cardId == rightNeighbor))
        {
            corruptedCount++;
        }

        countCorruptedList.Remove(corruptedCount);
        var randomIndex = Random.Range(0, countCorruptedList.Count);
        
        return $"{countCorruptedList[randomIndex]} Corrupted adjacent to me";
    }
}
