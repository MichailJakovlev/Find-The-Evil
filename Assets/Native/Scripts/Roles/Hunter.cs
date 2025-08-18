using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunter : Role
{
    public override string SendMessage()
    {
        if (_roleType == "Evil" || _cardStatus == "corrupted")
        {
            return SayLie(_cardNumber);
        }
        else
        {
            return SayTruth();
        }
    }

    public override void UseAbility()
    {
        if (_canUseAbility && _cardStatus != "corrupted")
        {
            Ability();
        }
    }

    public override string SayTruth()
    {
        var evilsList = _roleDirector.evils;
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
        
        return $"I am {minDistance} card away from closest Evil";
    }

    public override string SayLie(int evilCardNumber)
    {
        var evilsList = _roleDirector.evils;
        var villagerList = _roleDirector.villagers;
        var outcastList = _roleDirector.outcasts;
        var mergedLists = villagerList.Concat(outcastList).ToList();
        List<int> goodListDistance = new List<int>();
        List<int> evilListDistance = new List<int>();

        var minDistance = _roleDirector.cardPool._cardAmount;
        
        foreach (var evil in evilsList)
        {
            int distance1 = Mathf.Abs(evilCardNumber - evil.cardId);
            int distance2 = Mathf.Abs(evilCardNumber - (evil.cardId + _roleDirector.cardPool._cardAmount));
            int distance3 = Mathf.Abs(evilCardNumber - (evil.cardId - _roleDirector.cardPool._cardAmount));
            
            int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));
            if (distance != 0 && !evilListDistance.Contains(distance))
            {
                evilListDistance.Add(distance);
            }
        }
        foreach (var goodCard in mergedLists)
        {
            int distance1 = Mathf.Abs(evilCardNumber - goodCard.cardId);
            int distance2 = Mathf.Abs(evilCardNumber - (goodCard.cardId + _roleDirector.cardPool._cardAmount));
            int distance3 = Mathf.Abs(evilCardNumber - (goodCard.cardId - _roleDirector.cardPool._cardAmount));
            
            int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));
            if (distance != 0 && !goodListDistance.Contains(distance))
            {
                goodListDistance.Add(distance);
            }
        }
        List<int> differenceList = goodListDistance.Except(evilListDistance).ToList();
        var randomIndex = Random.Range(0, differenceList.Count);
        return $"I am {differenceList[randomIndex]} card away from closest Evil";
    }
   
    public override void Ability() { }
}
