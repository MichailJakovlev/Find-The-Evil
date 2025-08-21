public class Confessor : Role
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
        return "I am good";
    }

    public override string SayLie(int evilCardNumber)
    {
        return "I am dizzy"; 
    }
}
