using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Card Information")]
    public Role _cardRole;
    [HideInInspector]
    public TextMeshProUGUI _cardName;
    public int cardId;
    [HideInInspector]
    public SpriteRenderer _cardImage;
    [HideInInspector]
    public TextMeshProUGUI _cardNumber;
    [HideInInspector]
    public TextMeshProUGUI _cardMessageText;
    [HideInInspector]
    public CardDescription _cardDescription;
    [HideInInspector]
    public CardMessage _cardMessage;
    
    [HideInInspector]
    [SerializeField] private CardFlipAnimation _cardFlipAnimation;
    
    [Header("Message Zones")]
    public GameObject[] MessageZone;
    
    private UIHandler _uiHandler;
    private Health _health;
    private RoundDirector _roundDirector;
    public void OnEnable()
    {
        _uiHandler = FindObjectOfType<UIHandler>();
        _health = FindObjectOfType<Health>();
        _roundDirector = FindObjectOfType<RoundDirector>();
        EventBus.RestartRound += ClearCard;
    }

    private void OnDisable()
    {
        EventBus.RestartRound -= ClearCard;
    }

    public void Init()
    {
        if (_cardRole._roleType == "Evil")
        {
            _cardName.text = _cardRole._substituteRole._cardName;
            if (_cardRole._substituteRole._canShowMessage == false)
            {
                _cardRole._canShowMessage = false;
            }
            if (_cardRole._substituteRole._cardName == "Knight")
            {
                _cardRole._canShowMessage = false;
            }
            if (_cardRole._substituteRole._cardName == "Robber")
            {
                StartCoroutine(WaitInits(true));
            }
        }
        else
        {
            _cardName.text = _cardRole._cardName;
        }

        if (_cardRole._roleImage != null)
        {
            _cardFlipAnimation.cardImageSpriteRenderer.sprite = _cardRole._roleImage;
        }

        if (_cardRole._substituteRole._roleType == "Villager")
        {
            _cardDescription._roleTypeBackground.GetComponent<Image>().color = new Color(46 / 255f, 143 / 255f, 24 / 255f);
        }
        else
        {
            _cardDescription._roleTypeBackground.GetComponent<Image>().color = new Color(168 / 255f, 143 / 255f, 19 / 255f);
        }
        _cardMessage.gameObject.SetActive(false);
        
        if (_cardRole._cardName == "Witch Doctor")
        {
            StartCoroutine(WaitInits());
        }
        else if (_cardRole._cardName == "Robber")
        {
            StartCoroutine(WaitInits());
        }
        else
        {
            _cardRole.PassiveAbility();
        }
    }

    public IEnumerator WaitInits(bool isSubstituteRole = false)
    {
        yield return new WaitUntil(() => _cardRole._roleDirector.isEndAsignRoles);
        
        if (isSubstituteRole)_cardRole._substituteRole.PassiveAbility(_cardRole._card.cardId);
        else _cardRole.PassiveAbility();
        
        _roundDirector.RoundStart();
    }

    public void ShowMessage()
    {
        if (_cardRole._canUseAbility == false && _cardRole._substituteRole._canUseAbility == false) 
        {
            if (_cardRole._canShowMessage || _cardRole._substituteRole._canShowMessage)
            {
                _cardMessage.gameObject.SetActive(true);
                _cardMessageText.text = _cardRole.SendMessage();
            }
        }
        else
        {
            _cardMessage.gameObject.SetActive(false);
        }
    }

    public void KillCard()
    {
        if (_cardRole._roleType == "Evil")
        {
            _cardName.text = _cardRole._cardName;
            _cardName.color = Color.white;
            _cardFlipAnimation.cardStrokeFrontSpriteRenderer.color = Color.black;
            _cardFlipAnimation.cardImageSpriteRenderer.color = Color.red;
            _cardDescription._roleTypeBackground.GetComponent<Image>().color = new Color(168 / 255f, 19 / 255f, 24 / 255f);
            _cardDescription._cardRoleTypeText.text = _cardRole._roleType;
            _cardDescription._cardDescriptionText.text = _cardRole._cardInfoText;
            
            if (_cardRole._substituteRole._cardName == "Robber")
            {
                if (_health.currentHealth == 0)
                {
                    _roundDirector.RoundLose();
                }
                _cardRole._substituteRole.PassiveAbility(_cardRole._card.cardId);
            
            }
            
            _roundDirector.evilsKilled++;
            Debug.Log(_roundDirector.evilsKilled);
            if (_roundDirector.evilsKilled == _roundDirector.roleDirector.trueEvilsCards.Count)
            {
                _roundDirector.RoundWin();
            }
        }
        
        else if (_cardRole._roleType != "Evil" && _cardRole._isCorrupted == false)
        {
            var damageAmount = _cardRole._penaltyDamage;
            damageAmount = IsVoodooAbility(damageAmount);
            if (_cardRole._cardName == "Robber")
            {
                _cardRole.PassiveAbility();
            }
        
            if (_cardRole._cardName == "Knight" && !_cardRole._isCorrupted)
            {
                _cardRole._canShowMessage = true;
                ShowMessage();
                damageAmount = 0;
            }
            if (_cardRole._cardName == "Bomber" && !_cardRole._isCorrupted)
            {
                damageAmount = _health.currentHealth;
            }
            
            _health.Damage(damageAmount);
            damageAmount = 0;
            if (_health.currentHealth == 0)
            {
                _roundDirector.RoundLose();
            }
            
            Debug.Log("Is not Evil");
        }
        
        else
        {
            var damageAmount = _cardRole._penaltyDamage;
            damageAmount = IsVoodooAbility(damageAmount);
            
            if (_cardRole._cardName == "Knight")
            {
                _cardMessageText.gameObject.SetActive(false);
            }
            
            _health.Damage(damageAmount);
            if (_health.currentHealth == 0)
            {
                _roundDirector.RoundLose();
            }
            
            Debug.Log("Is not Evil");
        }
        _uiHandler.KillMode();
    }
    
    public void ClearCard() 
    {
        _cardName.color = Color.black;
        _cardFlipAnimation.cardStrokeFrontSpriteRenderer.color = Color.white;
        _cardFlipAnimation.cardImageSpriteRenderer.color = Color.white;
    }

    private int IsVoodooAbility(int damageAmount)
    {
        if (_cardRole._cardName != "Knight")
        {
            foreach (var card in _cardRole._roleDirector.evilsCards)
            {
                if (card._cardRole._cardName == "Voodoo")
                {
                    if (card._cardRole._cardHandler.isKilled == false)
                    {
                        damageAmount += card._cardRole._abilityDamage;
                    }
                }
            }
        }
        return damageAmount;
    }
}

