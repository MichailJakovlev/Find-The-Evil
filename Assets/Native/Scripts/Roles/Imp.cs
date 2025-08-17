public class Imp : Role
{
    public override string SendMessage()
    {
        if (_roleType == "Evil" || _cardStatus == "Corrupted")
        {
            return SayLie();
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

    public override string SayLie()
    {
        return _substituteRole.SayLie(); 
    }
   
    public override void Ability() { }
}
