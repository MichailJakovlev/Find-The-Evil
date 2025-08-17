using System;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class KillCard : MonoBehaviour
{
    [SerializeField] private Button _button;
    public bool _isKillMode = false;
    
    public void KillModeSwitch()
    {
        if (_isKillMode)
        {
            _isKillMode = false;
            _button.image.color = Color.gray;
        }
        else
        {
            _isKillMode = true;
            _button.image.color = Color.red;
        }
    }
}
