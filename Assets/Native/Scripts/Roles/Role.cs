using UnityEngine;

public abstract class Role : MonoBehaviour
{   
   [HideInInspector]
   public int _cardNumber;
   public Role _substituteRole;
   [HideInInspector]
   public RoleDirector _roleDirector;
   public AbilityInfo _abilityInfo;
   
   public string _cardName;
   public string _cardStatus;
   public string _roleType;
   public Sprite _roleImage;
   public bool _canUseAbility;

   private void Awake()
   {
      if (_roleType == "Villager")
      {
         _substituteRole = this;
      }
   }
   
   public virtual string SendMessage()
   {
      return " ";
   }

   public virtual void UseAbility()
   {
      
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
