using TMPro;
using UnityEngine;
public class Card : MonoBehaviour
{
    public Role _cardRole;
    public SpriteRenderer _cardImage;
    public TextMeshProUGUI _cardName;
    public TextMeshProUGUI _cardNumber;
    public TextMeshProUGUI _cardMessageText;
    public CardMessage _cardMessage;
    [SerializeField] private CardHandler _cardHandler;
    public int cardId;

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
    }

    public void ShowMessage()
    {
        _cardMessage.gameObject.SetActive(true);
        _cardMessageText.text = _cardRole.SendMessage();
    }

    public void KillCard()
    {
        if (_cardRole._roleType == "Evil")
        {
            _cardName.text = _cardRole._cardName;
            _cardName.color = Color.white;
            _cardHandler.cardStrokeFrontSpriteRenderer.color = Color.black;
            _cardHandler.cardImageSpriteRenderer.color = Color.red;
        }
        else
        {
            Debug.Log("Is not Evil");
        }
    }
    
    
}

