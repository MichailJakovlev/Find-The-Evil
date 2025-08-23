using UnityEngine;

public class CardInfo : MonoBehaviour
{
    public GameObject _info;

    public void ChangePosition(GameObject infoZone)
    {
        _info.transform.position = infoZone.transform.position;
    }
}
