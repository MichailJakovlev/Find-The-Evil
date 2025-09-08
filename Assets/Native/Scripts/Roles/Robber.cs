using System.Linq;
using UnityEngine;

public class Robber : Role
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

        return $" ";
    }

    public override string SayLie(int evilCardNumber)
    {
        
        return $" ";
    }

    public override void PassiveAbility()
    {
        if (_card._cardRole._cardHandler.isKilled == false)
        {
            var villagerList = _roleDirector.villagersCards;
            var outcastList = _roleDirector.outcastsCards;
            var evilList = _roleDirector.evilsCards;
            var mergedLists = villagerList.Concat(outcastList).ToList().Concat(evilList).ToList();

            var cardAmount = _roleDirector.cardPool._cardAmount;
            int rightNeighbor = _cardNumber % cardAmount + 1;

            foreach (var card in mergedLists)
            {
                if (card.cardId == rightNeighbor)
                {
                    card._cardRole._cardHandler.isFlippable = false;
                }
            }
        }
        else
        {
            var villagerList = _roleDirector.villagersCards;
            var outcastList = _roleDirector.outcastsCards;
            var evilList = _roleDirector.evilsCards;
            var mergedLists = villagerList.Concat(outcastList).ToList().Concat(evilList).ToList();

            var cardAmount = _roleDirector.cardPool._cardAmount;
            int rightNeighbor = _cardNumber % cardAmount + 1;

            foreach (var card in mergedLists)
            {
                if (card.cardId == rightNeighbor)
                {
                    card._cardRole._cardHandler.isFlippable = true;
                }
            }
        }
    }

    public override void PassiveAbility(int evilCardNumber)
    {
        if (_card._cardRole._cardHandler.isKilled == false)
        {
            var villagerList = _roleDirector.villagersCards;
            var outcastList = _roleDirector.outcastsCards;
            var evilList = _roleDirector.evilsCards;
            var mergedLists = villagerList.Concat(outcastList).ToList().Concat(evilList).ToList();
        
            var cardAmount = _roleDirector.cardPool._cardAmount;
            int rightNeighbor = evilCardNumber % cardAmount + 1;

            foreach (var card in mergedLists)
            {
                if (card.cardId == rightNeighbor)
                {
                    card._cardRole._cardHandler.isFlippable = false;
                }
            }
        }
        else
        {
            var villagerList = _roleDirector.villagersCards;
            var outcastList = _roleDirector.outcastsCards;
            var evilList = _roleDirector.evilsCards;
            var mergedLists = villagerList.Concat(outcastList).ToList().Concat(evilList).ToList();
        
            var cardAmount = _roleDirector.cardPool._cardAmount;
            int rightNeighbor = evilCardNumber % cardAmount + 1;

            foreach (var card in mergedLists)
            {
                if (card.cardId == rightNeighbor)
                {
                    card._cardRole._cardHandler.isFlippable = true;
                }
            }
        }
    }
   
}
