using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Role : MonoBehaviour
{   
   [HideInInspector]
   public int _cardNumber;
   [HideInInspector]
   public Role _substituteRole;
   [HideInInspector]
   public RoleDirector _roleDirector;
   [HideInInspector]
   public CardHandler _cardHandler;
   [HideInInspector]
   public Card _card;
   
   [Header("Card Settings")]
   public string _cardName;
   public string _roleType;
   public int _selectCardAmount;
   public Sprite _roleImage;
   public bool _canShowMessage;
   public bool _canUseAbility;
   public bool _isCorrupted;
   [TextArea]
   public string _cardInfoText;
   
   
   private void Awake()
   {
      if (_roleType == "Villager" || _roleType == "Outcast")
      {
         _substituteRole = this;
      }
   }

   private void Start()
   {
      if (_roleType == "Evil")
      {
         _substituteRole._roleType = "Evil";
         _selectCardAmount = _substituteRole._selectCardAmount;
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
   
   public virtual void PassiveAbility() { }
}
