using System.Collections.Generic;
using System.Linq;

public class Manipulator : Role
{
    public override string SendMessage()
    {
        return SayLie(_cardNumber);
    }
    
    public override string SayLie(int evilCardNumber)
    {
        return _substituteRole.SayLie(evilCardNumber);
    }

    public override void PassiveAbility()
    {
        var villagerList = _roleDirector.villagersCards;
        var outcastList = _roleDirector.outcastsCards;
        
        List<Card> mergedList = villagerList.Concat(outcastList).ToList();
        
        var cardAmount = _roleDirector.cardPool._cardAmount;
        
        int leftNeighbor = (_cardNumber - 2 + cardAmount) % cardAmount + 1;
        int rightNeighbor = _cardNumber % cardAmount + 1;
        int randomRange = 0;
        int randomMin = -1;
        int randomMax = 0;

        Card[] neighbors = new Card[2];

        if (mergedList.Any(good => good.cardId == leftNeighbor))
        {
            neighbors[0] = mergedList.First(good => good.cardId == leftNeighbor);
            randomRange ++;
            if (randomRange == 1)
            {
                randomMin = 0;
            }
        }
        
        if (mergedList.Any(good => good.cardId == rightNeighbor))
        {
            neighbors[1] = mergedList.First(good => good.cardId == rightNeighbor);
            randomRange ++;
        }
        if (randomRange == 2 && randomMin == 0)
        {
            randomMax = 1;
            int random = UnityEngine.Random.Range(randomMin, randomMax);
            neighbors[random]._cardRole._isCorrupted = true;
        }
        else if (randomRange == 1 && randomMin == 0)
        {
            randomMax = 0;
            int random = UnityEngine.Random.Range(randomMin, randomMax);
            neighbors[random]._cardRole._isCorrupted = true;
        }
        else if (randomRange == 1 && randomMin == -1)
        {
            randomMin = 1;
            randomMax = 1;
            int random = UnityEngine.Random.Range(randomMin, randomMax);
            neighbors[random]._cardRole._isCorrupted = true;
        }
    }
}
