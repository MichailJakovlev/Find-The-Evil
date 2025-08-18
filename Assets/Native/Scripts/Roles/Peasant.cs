using UnityEngine;

public class Peasant : Role
{
    public override string SendMessage()
    {
        if (_roleType == "Evil" || _cardStatus == "corrupted")
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
        if (_canUseAbility && _cardStatus != "corrupted")
        {
            Ability();
        }
    }

    public override string SayTruth()
    {
        return "I am villager";
    }

    public override string SayLie()
    {
        return "I am villager";
    }
   
    public override void Ability() { }
}
