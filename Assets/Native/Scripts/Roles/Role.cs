using UnityEngine;

public abstract class Role : MonoBehaviour
{   
   public int _cardNumber;
   public string _cardName;
   public string _cardStatus;
   public string _roleType;
   public Sprite _roleImage;
   public bool _canUseAbility;
   
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
