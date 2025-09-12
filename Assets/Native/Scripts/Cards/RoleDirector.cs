using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoleDirector : MonoBehaviour
{
    [Header("Object Links")]
    public CardPool cardPool;
    public ComplexityDirector complexityDirector;

    [Header("Created Roles")]
    public List<Role> createdVillagersRoles;
    public List<Role> createdOutcastsRoles;
    public List<Role> createdEvilsRoles;
    public List<Role> createdSubstituteRoles;

    [Header("Created Cards")] 
    public List<Card> villagersCards;
    public List<Card> outcastsCards;
    public List<Card> evilsCards;
    public List<Card> substituteCards;
    public List<Card> trueEvilsCardsForCount;

    private AbilityInfo _abilityInfo;
    [HideInInspector]
    public List<Role> mergedList;
    [HideInInspector]
    public bool isEndAsignRoles;

    public void AssignRoles()
    {
        isEndAsignRoles = false;
        _abilityInfo = FindObjectOfType<AbilityInfo>();
        
       ImplimentEvil();
       mergedList = createdVillagersRoles.Concat(createdOutcastsRoles).Concat(createdSubstituteRoles).ToList(); 
       
        for (int i = 0; i < cardPool._cardAmount; i++)
        {
            int rand = Random.Range(0, cardPool._cardAmount - i); 
            cardPool.cards[i]._cardRole = Instantiate(mergedList[rand], cardPool.cards[i].transform.GetChild(0).transform);
            mergedList.RemoveAt(rand);
        }

        for (int i = 0; i < cardPool._cardAmount; i++)
        {
            if (cardPool.cards[i]._cardRole._cardName == "Vagabond")
            {
                evilsCards.Add(cardPool.cards[i]);
            }
            
            if (cardPool.cards[i]._cardRole._roleType == "Evil")
            {
                evilsCards.Add(cardPool.cards[i]);
                substituteCards.Add(cardPool.cards[i]);
                trueEvilsCardsForCount.Add(cardPool.cards[i]);
            }   
            
            else if (cardPool.cards[i]._cardRole._roleType == "Outcast" && cardPool.cards[i]._cardRole._cardName != "Vagabond")
            {
                outcastsCards.Add(cardPool.cards[i]);
            }
            
            else if(cardPool.cards[i]._cardRole._cardName != "Vagabond") 
            {
                villagersCards.Add(cardPool.cards[i]);
            }
        }
        
        for (int i = 0; i < cardPool._cardAmount; i++)
        {
            cardPool.cards[i]._cardRole._roleDirector = this;
            cardPool.cards[i]._cardRole._substituteRole._roleDirector = this;
            cardPool.cards[i]._cardRole._card = cardPool.cards[i];
            cardPool.cards[i]._cardRole._substituteRole._card = cardPool.cards[i];
            cardPool.cards[i]._cardRole._cardHandler = cardPool.cards[i].GetComponentInChildren<CardHandler>();
            cardPool.cards[i]._cardDescription._cardDescriptionText.text = cardPool.cards[i]._cardRole._substituteRole._cardInfoText;
            cardPool.cards[i]._cardDescription._cardRoleTypeText.text = cardPool.cards[i]._cardRole._substituteRole._roleType;
            cardPool.cards[i]._cardRole._substituteRole._cardHandler = cardPool.cards[i]._cardRole._cardHandler;
            cardPool.cards[i]._cardRole._cardNumber = (cardPool._cardAmount - i);
            cardPool.cards[i]._cardMessageText =  cardPool.cardMessages[i].transform.GetComponentInChildren<TextMeshProUGUI>();
            cardPool.cardMessages[i].SetActive(true);
            cardPool.cards[i].Init();
        }

        isEndAsignRoles = true;
    }
    public void ImplimentEvil()
    {
        for (int i = 0; i < createdEvilsRoles.Count; i++)
        {
            Role role = Instantiate(createdSubstituteRoles[i]);
            createdSubstituteRoles[i] = Instantiate(createdEvilsRoles[i]);
            createdSubstituteRoles[i]._substituteRole = role;
        }
    }
    
    public void ClearRoles()
    {
        for (int i = 0; i < villagersCards.Count; i++)
        {
            Destroy(villagersCards[i]._cardRole.gameObject);
        }
        createdVillagersRoles.Clear();
        villagersCards.Clear();
        
        for (int i = 0; i < outcastsCards.Count; i++)
        {
            Destroy(outcastsCards[i]._cardRole.gameObject);
        }
        createdOutcastsRoles.Clear();
        outcastsCards.Clear();
        
        for (int i = 0; i < evilsCards.Count; i++)
        {
           Destroy(evilsCards[i]._cardRole.gameObject);
        }
        createdEvilsRoles.Clear();
        evilsCards.Clear();

        for (int i = 0; i < createdSubstituteRoles.Count; i++)
        {
            Destroy(substituteCards[i]._cardRole.gameObject);
        }
        substituteCards.Clear();
        createdSubstituteRoles.Clear();
        
        for (int i = 0; i < mergedList.Count; i++)
        {
            Destroy(mergedList[i].gameObject);
            
        }
        mergedList.Clear();
        trueEvilsCardsForCount.Clear();
        
        complexityDirector.SetRoundComplexity();
    }
}
