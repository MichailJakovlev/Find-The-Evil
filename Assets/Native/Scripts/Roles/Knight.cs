using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knight : Role
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
        return $"I can't die";
    }

    public override string SayLie(int evilCardNumber)
    {
        return $"";
    }
}
