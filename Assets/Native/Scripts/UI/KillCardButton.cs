using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KillCardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void OnEnable()
    {
        EventBus.KillModeOn += KillModeOn;
        EventBus.KillModeOff += KillModeOff;
        EventBus.RestartRound += Hide;
    }

    private void OnDisable()
    {
        EventBus.KillModeOn -= KillModeOn;
        EventBus.KillModeOff -= KillModeOff;
        EventBus.RestartRound -= Hide;
    }

    public void Hide()
    {
        _button.image.enabled = false;
        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        yield return new WaitForSeconds(1.2f);
        _button.image.enabled = true;
    }
    
    public void KillModeOn()
    {
        _button.image.color = Color.red;
    }

    public void KillModeOff()
    {
        _button.image.color = Color.gray;
    }
}
