using UnityEngine;

public class Judge : Role
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
        Debug.Log("Ability");
        _abilityInfo.ShowAbilityInfo("Pick 1 card");
    }

    public override string SayTruth()
    {
        int rand = Random.Range(0, _roleDirector.villagers.Count);
        return "#" + _roleDirector.villagers[rand]._cardRole._cardNumber + " is real " + _roleDirector.villagers[rand]._cardRole._cardName;
    }

    public override string SayLie(int evilCardNumber)
    {
        int rand = Random.Range(0, _roleDirector.evils.Count);
        return "#" + _roleDirector.evils[rand]._cardRole._cardNumber + " is real " + _roleDirector.evils[rand]._cardRole._substituteRole._cardName;
    }

    public override void Ability()
    {
        SendMessage();
    }
}
