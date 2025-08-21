public class Imp : Role
{
    public override string SendMessage()
    {
        return SayLie(_cardNumber);
    }
    
    public override string SayLie(int evilCardNumber)
    {
        return _substituteRole.SayLie(evilCardNumber);
    }
}
