using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class AbilityInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInfo;
    [SerializeField] private GameObject _background;

    void Start()
    {
        _background.SetActive(false);
        _textInfo.enabled = false;
    }
    public void ShowAbilityInfo(string text)
    {
        _background.SetActive(true);
        _textInfo.enabled = true;
        _textInfo.text = text;
    }

    public void HideAbilityInfo()
    {
        _background.SetActive(false);
        _textInfo.enabled = false;
    }
}
