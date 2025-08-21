using Random = UnityEngine.Random;

public class Medium : Role
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
        return "#" + _roleDirector.villagersCards[rand]._cardRole._cardNumber + " is real " + _roleDirector.villagersCards[rand]._cardRole._cardName;
    }

    public override string SayLie(int evilCardNumber)
    {
        int rand = Random.Range(0, _roleDirector.evilsCards.Count);
        return "#" + _roleDirector.evilsCards[rand]._cardRole._cardNumber + " is real " + _roleDirector.evilsCards[rand]._cardRole._substituteRole._cardName;
    }
}
