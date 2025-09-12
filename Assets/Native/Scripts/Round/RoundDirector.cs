using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundDirector : MonoBehaviour
{
    [HideInInspector] public Health health;
    [HideInInspector] public RoleDirector roleDirector;
    
    public List<Card> closedCardsList = new List<Card>();
    public List<Card> cardsInRoundList = new List<Card>();

    public int openedCards;
    public int evilsKilled;

    private void Start()
    {
        health = FindObjectOfType<Health>();
        roleDirector = FindObjectOfType<RoleDirector>();

    }
    
    public IEnumerator WaitAsignRoles()
    {
        yield return new WaitUntil(() => roleDirector.isEndAsignRoles);
        RoundStart();
    }

    public void RoundStart()
    {
        cardsInRoundList = roleDirector.villagersCards
            .Concat(roleDirector.outcastsCards).ToList()
            .Concat(roleDirector.evilsCards).ToList();
        closedCardsList = cardsInRoundList.ToList();
        health.Heal();
        evilsKilled = 0;
        openedCards = 0;
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
