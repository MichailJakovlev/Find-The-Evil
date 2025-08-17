using TMPro;
using UnityEngine;
public class Card : MonoBehaviour
{
    public Role _cardRole;
    public SpriteRenderer _cardImage;
    public TextMeshProUGUI _cardName;
    public TextMeshProUGUI _cardNumber;
    public int cardId;

    void Start()
    {
        _cardName.text = _cardRole._cardName;
    }
    
    
    
    
}

