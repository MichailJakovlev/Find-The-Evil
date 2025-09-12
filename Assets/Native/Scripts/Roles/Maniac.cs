using System.Linq;
using UnityEngine;

public class Maniac : Role
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

        return $" ";
    }

    public override string SayLie(int evilCardNumber)
    {
        
        return $" ";
    }

    public override void PassiveAbility()
    {
        
    }

}
