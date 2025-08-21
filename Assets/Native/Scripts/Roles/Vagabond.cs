using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Vagabond : Role
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

    public override string SayTruth()
    {

        return $" ";
    }

    public override string SayLie(int evilCardNumber)
    {
        
        return $" ";
    }
}
