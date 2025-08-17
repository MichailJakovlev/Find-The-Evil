using Unity.VisualScripting;
using UnityEngine;

public class gitCardPool : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Card[] cards;
    
    public int _maxCardAmount;
    public int _cardAmount;
    
    private Vector3 _axis;
    void Start()
    {
        cards = new Card[_maxCardAmount];
        Create();
    }

    public void Create()
    {
        for (int i = 0; i < _maxCardAmount; i++)
        {
            cards[i] = Instantiate(cardPrefab, transform);
            cards[i].gameObject.SetActive(false);
        }

        Get(_cardAmount);
    }
    
    public void Get(int cardAmount)
    {
        for (int i = 0; i < cardAmount; i++)
        {
            cards[i].gameObject.SetActive(true);
            cards[i]._cardNumber.text = (cardAmount - i).ToString();
            cards[i].cardId = (cardAmount - i);
        }
        PositionChanger(cardAmount);
    }

    public void PositionChanger(int value)
    {
        int shiftValue = (value - 1) / 2;
        for (int i = 0; i < value; i++)
        {
            float _rotateValue = 360 / value * i;
            _axis.z = _rotateValue;
            
            if (360 % value != 0 && i > (value - 1) / 2)
            {
                _rotateValue = 360 / value * shiftValue;
                shiftValue--;
                _axis.z = -_rotateValue;
            }
            
              cards[i].gameObject.transform.rotation = new Quaternion(0,0,0,0);
              cards[i].gameObject.transform.Rotate(_axis);
              cards[i]._cardImage.GameObject().transform.Rotate(-_axis);
              
        }
    }
    
    public void Realize()
    {
        for (int i = 0; i < _maxCardAmount; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
    }
}
