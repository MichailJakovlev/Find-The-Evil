using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

public class Gawker : Role
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
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        var mergedList = villagerList.Concat(outcastList).ToList();

        var corruptedList = new List<Card>();

        var message = "";

        foreach (var card in mergedList)
        {
            if (card._cardRole._isCorrupted)
            {
                corruptedList.Add(card);
            }
        }

        if (corruptedList.IsEmpty())
        {
            return "There's no Corrupted characters!";
        }
        else
        {
            var randomIndex = Random.Range(0, corruptedList.Count);
            message = $"{corruptedList[randomIndex]._cardNumber.text} is Corrupted";
        }
        
        return message;
    }

    public override string SayLie(int evilCardNumber)
    {
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        var mergedList = villagerList.Concat(outcastList).ToList();

        var corruptedList = new List<Card>();
        var messageList = new List<string>();

        var message = "";

        foreach (var card in mergedList)
        {
            if (card._cardRole._isCorrupted)
            {
                corruptedList.Add(card);
            }
        }

        if (corruptedList.IsEmpty())
        {
            var randomIndex = Random.Range(0, mergedList.Count);
            message = $"{mergedList[randomIndex]._cardNumber.text} is Corrupted";
        }
        else
        {
            var someList = mergedList.Except(corruptedList).ToList();
            var randomIndex = Random.Range(0, someList.Count);
            
            messageList.Add($"{someList[randomIndex]._cardNumber.text} is Corrupted");
            messageList.Add("There's no Corrupted characters!");
            
            var randomIndexB = Random.Range(0, messageList.Count);
            message = $"{messageList[randomIndexB]}";
        }
        
        return message;
    }
}
