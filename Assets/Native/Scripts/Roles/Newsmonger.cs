using UnityEngine;

public class Newsmonger : Role
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
        int rand = Random.Range(0, _roleDirector.villagersCards.Count);
        return "#" + _roleDirector.villagersCards[rand]._cardRole._cardNumber + " is good";
    }

    public override string SayLie(int evilCardNumber)
    {
        int rand = Random.Range(0, _roleDirector.evilsCards.Count);
        return "#" + _roleDirector.evilsCards[rand]._cardRole._cardNumber + " is good";
    }
}
