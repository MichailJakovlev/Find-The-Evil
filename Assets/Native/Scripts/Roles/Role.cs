using UnityEngine;

public class Role : MonoBehaviour
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
   public int _selectCardAmount;
   public Card _card;
   
   private void Awake()
   {
      if (_roleType == "Villager" || _roleType == "Outcast")
      {
         _substituteRole = this;
      }
   }
   
   public virtual string SendMessage()
   {
      return " ";
   }
   
   public virtual string SayTruth()
   {
      return " ";
   }

   public virtual string SayLie(int evilCardNumber)
   {
      return " "; 
   }
   
   public virtual void Ability(params Card[] selectedCards) { }
}
