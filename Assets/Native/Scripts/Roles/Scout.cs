using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scout : Role
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
        var evilsList = _roleDirector.evilsCards;
        var cardAmount = _roleDirector.cardPool._cardAmount;
        var evelCount = 0;
        
        int leftNeighbor = (_cardNumber - 2 + cardAmount) % cardAmount + 1;
        int rightNeighbor = _cardNumber % cardAmount + 1;

        if (evilsList.Any(evil => evil.cardId == leftNeighbor))
        {
            evelCount++;
        }
        if (evilsList.Any(evil => evil.cardId == rightNeighbor))
        {
            evelCount++;
        }

        return $"{evelCount} Evil adjacent to me";
    }

    public override string SayLie(int evilCardNumber)
    {
        var evilsList = _roleDirector.evilsCards;
        var cardAmount = _roleDirector.cardPool._cardAmount;
        var evelCount = 0;
        List<int> countEvelList = new List<int>() { 0, 1, 2 };
        
        int leftNeighbor = (evilCardNumber - 2 + cardAmount) % cardAmount + 1;
        int rightNeighbor = evilCardNumber % cardAmount + 1;

        if (evilsList.Any(evil => evil.cardId == leftNeighbor))
        {
            evelCount++;
        }
        if (evilsList.Any(evil => evil.cardId == rightNeighbor))
        {
            evelCount++;
        }

        countEvelList.Remove(evelCount);
        var randomIndex = Random.Range(0, countEvelList.Count);
        
        return $"{countEvelList[randomIndex]} Evil adjacent to me";
    }
}
