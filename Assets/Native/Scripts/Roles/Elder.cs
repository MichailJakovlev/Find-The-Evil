using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Elder : Role
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
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        
        var mergedLists = villagerList.Concat(outcastList).ToList();
        mergedLists.Shuffle();
        
        List<Card> finalList = new List<Card>();
        
        finalList.Add(GetRandomElementFromList(evilsList));
        finalList.Add(mergedLists[0]);
        finalList.Add(mergedLists[1]);
        
        finalList.Shuffle();

        string answer = "";
        for (int i = 0; i < finalList.Count; i++)
        {
            if (i == finalList.Count - 1)
            {
                answer = answer + $"{finalList[i]._cardNumber.text}";
            }
            else
            {
                answer = answer + $"{finalList[i]._cardNumber.text}, ";
            }
            
        }
        
        return $"One is Evil: {answer}";
    }

    public override string SayLie(int evilCardNumber)
    {
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        
        var mergedLists = villagerList.Concat(outcastList).ToList();
        mergedLists.Shuffle();
        
        List<Card> finalList = new List<Card>();
        
        finalList.Add(mergedLists[0]);
        finalList.Add(mergedLists[1]);
        finalList.Add(mergedLists[2]);
        
        finalList.Shuffle();
        
        string answer = "";
        for (int i = 0; i < finalList.Count; i++)
        {
            if (i == finalList.Count - 1)
            {
                answer = answer + $"{finalList[i]._cardNumber.text}";
            }
            else
            {
                answer = answer + $"{finalList[i]._cardNumber.text}, ";
            }
            
        }
        
        return $"One is Evil: {answer}";
    }
    
    private T GetRandomElementFromList<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogError("List is null or empty!");
            return default(T);
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
