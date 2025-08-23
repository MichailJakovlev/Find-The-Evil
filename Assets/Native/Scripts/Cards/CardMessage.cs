using TMPro;
using UnityEngine;

public class CardMessage : MonoBehaviour
{
    public GameObject _cardMessage;
    public GameObject _messageBackground;
    public TextMeshProUGUI _cardMessageText;

    public void ChangePosition(GameObject messageZone)
    {
        _cardMessage.transform.position = messageZone.transform.position;
    }
}
