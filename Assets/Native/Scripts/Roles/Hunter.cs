using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunter : Role
{
    public override string SendMessage()
    {
        if (_roleType == "Evil" || _cardStatus == "corrupted")
        {
            return SayLie();
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
        Debug.Log(_roleType + " " +_cardNumber);

        foreach (var evil in evilsList)
        {
            int d1 = Mathf.Abs(_cardNumber - evil.cardId);
            int d2 = Mathf.Abs(_cardNumber - (evil.cardId + _roleDirector.cardPool._cardAmount));
            int d3 = Mathf.Abs(_cardNumber - (evil.cardId - _roleDirector.cardPool._cardAmount));
            
            int d = Mathf.Min(d1, Mathf.Min(d2, d3));

            if (d < minDistance)
            {
                minDistance = d;
            }
        }
        
        return $"I am {minDistance} card away from closest Evil";
    }

    public override string SayLie()
    {
        // var evilsList = _roleDirector.evils;
        // var villagerList = _roleDirector.villagers;
        // var outcastList = _roleDirector.outcasts;
        // var mergedLists = villagerList.Concat(outcastList).ToList();
        // List<int> goodListDistance = new List<int>();
        Debug.Log(_roleType + " " + _cardNumber);

        // var minDistance = _roleDirector.cardPool._cardAmount;
        //
        // foreach (var evil in evilsList)
        // {
        //     int distance1 = Mathf.Abs(_cardNumber - evil.cardId);
        //     int distance2 = Mathf.Abs(_cardNumber - (evil.cardId + _roleDirector.cardPool._cardAmount));
        //     int distance3 = Mathf.Abs(_cardNumber - (evil.cardId - _roleDirector.cardPool._cardAmount));
        //     
        //     int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));
        //     if (distance < minDistance && distance != 0)
        //     {
        //         minDistance = distance;
        //     }
        // }
        // foreach (var goodCard in mergedLists)
        // {
        //     int distance1 = Mathf.Abs(_cardNumber - goodCard.cardId);
        //     int distance2 = Mathf.Abs(_cardNumber - (goodCard.cardId + _roleDirector.cardPool._cardAmount));
        //     int distance3 = Mathf.Abs(_cardNumber - (goodCard.cardId - _roleDirector.cardPool._cardAmount));
        //     
        //     int distance = Mathf.Min(distance1, Mathf.Min(distance2, distance3));
        //     // Debug.Log(minDistance + ": " + _cardNumber);
        //     if (distance != minDistance && distance != 0)
        //     {
        //         goodListDistance.Add(distance);
        //     }
        // }
        // var randomIndex = Random.Range(0, goodListDistance.Count - 1);
        // // foreach (var goodCard in goodListDistance) Debug.Log(goodCard);
        return $" ";
    }
   
    public override void Ability() { }
}
