using UnityEngine;

public class Peasant : Role
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

    public override string SayTruth()
    {
        return "I am villager";
    }

    public override string SayLie(int evilCardNumber)
    {
        return "I am villager";
    }
}
