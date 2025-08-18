public class Imp : Role
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

    public override void UseAbility()
    {
        if (_canUseAbility && _cardStatus != "Corrupted")
        {
            Ability();
        }
    }

    public override string SayTruth()
    {
        return _substituteRole.SayTruth();
    }

    public override string SayLie(int evilCardNumber)
    {
        return _substituteRole.SayLie(_cardNumber); 
    }
   
    public override void Ability() { }
}
