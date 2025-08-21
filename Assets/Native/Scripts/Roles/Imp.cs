public class Imp : Role
{
    void Start()
    {
        _substituteRole._roleType = "Evil";
    }

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
        return _substituteRole.SayTruth();
    }

    public override string SayLie(int evilCardNumber)
    {
        return _substituteRole.SayLie(evilCardNumber);
    }
}
