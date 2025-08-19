using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardMessage cardMessagePrefab;
    [SerializeField] private GameObject[] cardMessages;
    [SerializeField] private RoleDirector roleDirector;
    
    public Card[] cards;
    public int _maxCardAmount;
    public int _cardAmount;
    
    private Vector3 _axis;
    private KillCardButton _killCardButton;
    
    private void OnEnable()
    {
        EventBus.RestartRound += Realize;
        
    }

    private void OnDisable()
    {
        EventBus.RestartRound -= Realize;
    }

    void Start()
    {
        _killCardButton = FindFirstObjectByType<KillCardButton>();
        _killCardButton.Hide();
        cards = new Card[_maxCardAmount];
        cardMessages = new GameObject[_maxCardAmount];
        
        Create();
    }

    public void Create()
    {
        for (int i = 0; i < _maxCardAmount; i++)
        {
            cards[i] = Instantiate(cardPrefab, transform);
            cardMessages[i] = Instantiate(cardMessagePrefab.gameObject, transform);
            cards[i]._cardMessage = cardMessages[i].GetComponent<CardMessage>();
            cardMessages[i].gameObject.SetActive(false);
            cards[i].gameObject.SetActive(false);
        }

        Get(_cardAmount);
    }
    
    public void Get(int cardAmount)
    {
        for (int i = 0; i < cardAmount; i++)
        {
            cards[i].gameObject.SetActive(true);
            cards[i]._cardNumber.text = "#" + (cardAmount - i);
            cards[i]._cardMessageText = cardMessages[i].transform.GetComponentInChildren<TextMeshProUGUI>();
            cardMessages[i].SetActive(true);
            cards[i].cardId = (cardAmount - i);
        }
        
        PositionChanger(cardAmount);
        roleDirector.AssignRoles();
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
              
              cardMessages[i].gameObject.transform.rotation = new Quaternion(0,0,0,0);
              cardMessages[i].gameObject.transform.Rotate(_axis);
              cardMessages[i].transform.GetChild(0).transform.Rotate(-_axis);
        }
    }
    
    public void Realize()
    {
        for (int i = 0; i < _maxCardAmount; i++)
        {
            cards[i].gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            cards[i]._cardImage.GameObject().transform.rotation = Quaternion.Euler(Vector3.zero);
            
            cardMessages[i].gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            cardMessages[i].transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
            
            cards[i].gameObject.SetActive(false);
            cardMessages[i].SetActive(false);   
        }
        roleDirector.ClearRoles();
        Get(6);
    }
}
