using System.Collections.Generic;
using System.Linq;

public class Lich : Role
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

        if (mergedList.Any(good => good.cardId == leftNeighbor))
        {
            mergedList.First(good => good.cardId == leftNeighbor)._cardRole._isCorrupted = true;
        }

        if (mergedList.Any(good => good.cardId == rightNeighbor))
        {
            mergedList.First(good => good.cardId == rightNeighbor)._cardRole._isCorrupted = true;
        }
    }
}
