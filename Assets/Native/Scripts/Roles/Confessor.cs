using UnityEngine;

public class Confessor : Role
{
    public string SendMessage()
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

    public void UseAbility()
    {
        if (_canUseAbility && _cardStatus != "corrupted")
        {
            Ability();
        }
    }

    public string SayTruth()
    {
        return " ";
      
    }

    public string SayLie()
    {
        return " "; 
    }
   
    public void Ability() { }
}
