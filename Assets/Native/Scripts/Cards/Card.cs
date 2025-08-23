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
    
    [Header("Object Links")]
    [SerializeField] private CardFlipAnimation _cardFlipAnimation;
    
    [Header("Message Zones")]
    public GameObject[] MessageZone;
    
    private UIHandler _uiHandler;
    public void OnEnable()
    {
        _uiHandler = FindObjectOfType<UIHandler>();
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
        _cardRole.PassiveAbility();
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
        }
        else
        {
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
}

