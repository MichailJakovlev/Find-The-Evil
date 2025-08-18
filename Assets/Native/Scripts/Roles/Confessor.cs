using UnityEngine;

public class Confessor : Role
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

    public override void UseAbility()
    {
        if (_canUseAbility && _cardStatus != "corrupted")
        {
            Ability();
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
   
    public override void Ability() { }
}
