using TMPro;
using UnityEngine;

public class HPView : MonoBehaviour
{
    private Health _health;
    
    [SerializeField] private TextMeshProUGUI hpText;

    private void Awake()
    {
        _health = FindObjectOfType<Health>();
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int currentHealth)
    {
        hpText.text = currentHealth.ToString();
    }
    
}
