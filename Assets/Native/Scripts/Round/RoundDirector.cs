using System;
using UnityEngine;

public class RoundDirector : MonoBehaviour
{
    [HideInInspector] public Health health;
    [HideInInspector] public RoleDirector roleDirector;
    // [HideInInspector] public Card card;
    
    public int evilsKilled;

    private void Start()
    {
        health = FindObjectOfType<Health>();
        roleDirector = FindObjectOfType<RoleDirector>();
    }

    public void RoundStart()
    {
        health.Heal();
        evilsKilled = 0;
        Debug.Log("Round Start");
    }
    
    public void RoundEnd()
    {
        Debug.Log("Round End");
    }
    
    public void RoundWin()
    {
        Debug.Log("Round Win");
        RoundEnd();
    }
    
    public void RoundLose()
    {
        Debug.Log("Round Lose");
        RoundEnd();
    }
    
}
