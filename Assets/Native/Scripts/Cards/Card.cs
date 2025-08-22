using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Role _cardRole;
    public SpriteRenderer _cardImage;
    public TextMeshProUGUI _cardName;
    public TextMeshProUGUI _cardNumber;
    [HideInInspector]
    public TextMeshProUGUI _cardMessageText;
    public CardMessage _cardMessage;
    public int cardId;
    
    [SerializeField] private CardFlipAnimation _cardFlipAnimation;
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
        _cardMessage.gameObject.SetActive(false);
        _cardRole.PassiveAbility();
    }

    public void ShowMessage()
    {
        if (_cardRole._canUseAbility == false && _cardRole._substituteRole._canUseAbility == false)
        {
            _cardMessage.gameObject.SetActive(true);
            _cardMessageText.text = _cardRole.SendMessage();
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
            if (_cardRole._cardName == "Knight")
            {
                _cardMessageText.gameObject.SetActive(false);
            }
        }
        else if (_cardRole._cardName == "Knight" && !_cardRole._isCorrupted)
        {
            ShowMessage();
        }
        else
        {
            if (_cardRole._cardName == "Knight")
            {
                _cardMessageText.gameObject.SetActive(false);
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
}

