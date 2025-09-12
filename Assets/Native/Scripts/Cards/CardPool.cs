using Unity.VisualScripting;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardMessage cardMessagePrefab;
    [SerializeField] public GameObject[] cardMessages;
    [SerializeField] private RoleDirector roleDirector;
    
    private RoundDirector roundDirector;
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
        roundDirector = FindObjectOfType<RoundDirector>();
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
            cards[i]._cardDescription = cardMessages[i].GetComponent<CardDescription>();
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
            
            cards[i].cardId = (cardAmount - i);
        }
        
        PositionChanger(cardAmount);
        roleDirector.AssignRoles();
        StartCoroutine(roundDirector.WaitAsignRoles()); // round start
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
              
              if (i > (value - 1) / 2)
              {
                  cardMessages[i].GetComponent<CardMessage>().ChangePosition(cards[i].MessageZone[0]); //right
                  cards[i]._cardDescription._descriptionRectTransform.localPosition = new Vector3(cards[i]._cardDescription._descriptionCanvas.sizeDelta.x / 2, 0f, 0f);
                  cards[i]._cardDescription.descriptionLayoutGroup.childAlignment = TextAnchor.UpperLeft;
              }
              else if(i == 0 && value % 2 != 0)
              {
                  cardMessages[i].GetComponent<CardMessage>().ChangePosition(cards[i].MessageZone[3]);// bottom
                  cards[i]._cardDescription._descriptionRectTransform.localPosition = new Vector3(0f, 0f, 0f);
                  cards[i]._cardDescription.descriptionLayoutGroup.childAlignment = TextAnchor.UpperCenter;
              }
              else if(i == 0 && value % 2 == 0)
              {
                  cardMessages[i].GetComponent<CardMessage>().ChangePosition(cards[i].MessageZone[3]);// bottom
                  cards[i]._cardDescription._descriptionRectTransform.localPosition = new Vector3(0f, 0f, 0f);
                  cards[i]._cardDescription.descriptionLayoutGroup.childAlignment = TextAnchor.UpperCenter;
              }
              else
              {
                  cardMessages[i].GetComponent<CardMessage>().ChangePosition(cards[i].MessageZone[1]); //left
                  cards[i]._cardDescription._descriptionRectTransform.localPosition = new Vector3(0f, 0f, 0f);
                  cards[i]._cardDescription._descriptionRectTransform.localPosition = new Vector3(cards[i]._cardDescription._descriptionCanvas.sizeDelta.x / 2 * -1, 0f, 0f);
                  cards[i]._cardDescription.descriptionLayoutGroup.childAlignment = TextAnchor.UpperRight;
              }
             
              if(i == value / 2 && value % 2 == 0)
              {
                  cardMessages[i].GetComponent<CardMessage>().ChangePosition(cards[i].MessageZone[2]); // top
                  cards[i]._cardDescription._descriptionRectTransform.localPosition = new Vector3(0f, 0f, 0f);
                  cards[i]._cardDescription.descriptionLayoutGroup.childAlignment = TextAnchor.UpperCenter;
              }
              
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
        Get(_cardAmount);
    }
}
