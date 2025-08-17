using UnityEngine;

public abstract class Role : MonoBehaviour
{   
   [HideInInspector]
   public int _cardNumber;
   [HideInInspector]
   public Role _substituteRole;
   [HideInInspector]
   public RoleDirector _roleDirector;
   
   public string _cardName;
   public string _cardStatus;
   public string _roleType;
   public Sprite _roleImage;
   public bool _canUseAbility;
   
   public virtual string SendMessage()
   {
      return " ";
   }

   public virtual void UseAbility()
   {
      if (_canUseAbility && _cardStatus != "corrupted")
      {
         Ability();
      }
   }

   public virtual string SayTruth()
   {
      return " ";
   }

   public virtual string SayLie()
   {
      return " "; 
   }
   
   public virtual void Ability() { }
}
