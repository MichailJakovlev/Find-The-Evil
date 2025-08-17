using UnityEngine;
using Random = UnityEngine.Random;

public class Medium : Role
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
        return "#" + _roleDirector.villagers[Random.Range(0, _roleDirector.villagers.Count)]._cardRole._cardNumber + " is good";
    }

    public override string SayLie()
    {
        return "#" + _roleDirector.evils[Random.Range(0, _roleDirector.evils.Count)]._cardRole._cardNumber + " is good";
    }
   
    public override void Ability() { }
}
