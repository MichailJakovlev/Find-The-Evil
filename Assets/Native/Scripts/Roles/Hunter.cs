using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunter : Role
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
        var minDistance = _roleDirector.cardPool._cardAmount;

        foreach (var evil in evilsList)
        {
            int distance1 = Mathf.Abs(_cardNumber - evil.cardId);
            int distance2 = Mathf.Abs(_cardNumber - (evil.cardId + _roleDirector.cardPool._cardAmount));
            int distance3 = Mathf.Abs(_cardNumber - (evil.cardId - _roleDirector.cardPool._cardAmount));
            
            int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));

            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        
        return $"I am {minDistance} card away from <b>closest</b> Evil";
    }

    public override string SayLie(int evilCardNumber)
    {
        var evilsList = _roleDirector.evilsCards;
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        var mergedLists = villagerList.Concat(outcastList).ToList().Concat(evilsList).ToList();
        List<int> goodListDistance = new List<int>();

        var minDistance = _roleDirector.cardPool._cardAmount;
        
        foreach (var evil in evilsList)
        {
            int distance1 = Mathf.Abs(evilCardNumber - evil.cardId);
            int distance2 = Mathf.Abs(evilCardNumber - (evil.cardId + _roleDirector.cardPool._cardAmount));
            int distance3 = Mathf.Abs(evilCardNumber - (evil.cardId - _roleDirector.cardPool._cardAmount));
            
            int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));
            if (distance != 0 && distance < minDistance)
            {
                minDistance = distance;
            }
        }
        
        foreach (var goodCard in mergedLists)
        {
            int distance1 = Mathf.Abs(evilCardNumber - goodCard.cardId);
            int distance2 = Mathf.Abs(evilCardNumber - (goodCard.cardId + _roleDirector.cardPool._cardAmount));
            int distance3 = Mathf.Abs(evilCardNumber - (goodCard.cardId - _roleDirector.cardPool._cardAmount));
            
            int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));
            if (distance != 0 && distance != minDistance && !goodListDistance.Contains(distance))
            {
                goodListDistance.Add(distance);
            }
        }
        
        var randomIndex = Random.Range(0, goodListDistance.Count);
        return $"I am {goodListDistance[randomIndex]} card away from <b>closest</b> Evil";
    }
}
