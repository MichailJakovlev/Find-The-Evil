using UnityEngine;
using Random = UnityEngine.Random;

public class Medium : Role
{
    public override string SendMessage()
    {
        if (_roleType == "Evil" || _cardStatus == "Corrupted")
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
        int rand = Random.Range(0, _roleDirector.villagers.Count);
        return "#" + _roleDirector.villagers[rand]._cardRole._cardNumber + " is real " + _roleDirector.villagers[rand]._cardRole._cardName;
    }

    public override string SayLie(int evilCardNumber)
    {
        int rand = Random.Range(0, _roleDirector.evils.Count);
        return "#" + _roleDirector.evils[rand]._cardRole._cardNumber + " is real " + _roleDirector.evils[rand]._cardRole._substituteRole._cardName;
    }
}
