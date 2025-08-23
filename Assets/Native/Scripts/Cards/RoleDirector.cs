using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoleDirector : MonoBehaviour
{
    [Header("Delete This")] // Временное поле
    [SerializeField] private int villagersAmount;
    [SerializeField] private int outcastsAmount;
    [SerializeField] private int evilsAmount;
    
    [Header("Roles")]
    [SerializeField] private List<Role> villagersRoles;
    [SerializeField] private List<Role> outcastsRoles;
    [SerializeField] private List<Role> evilsRoles;
    
    [Header("Object Links")]
    public CardPool cardPool;

    [Header("Created Roles")]
    public List<Role> createdVillagersRoles;
    public List<Role> createdOutcastsRoles;
    public List<Role> createdEvilsRoles;
    
    [Header("Created Cards")] 
    public List<Card> villagersCards;
    public List<Card> outcastsCards;
    public List<Card> evilsCards;

    private AbilityInfo _abilityInfo;
    private List<Role> mergedList;

    public bool isEndAsignRoles;

    public void AssignRoles()
    {
        isEndAsignRoles = false;
        _abilityInfo = FindObjectOfType<AbilityInfo>();
        
        CreateVillagers(villagersAmount);
        CreateOutcasts(outcastsAmount);
        CreateEvils(evilsAmount);
        
        mergedList = createdVillagersRoles.Concat(createdOutcastsRoles).ToList();
        ImplimentEvil();
        
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
            cardPool.cards[i].Init();
        }

        isEndAsignRoles = true;
    }
    
    public void CreateVillagers(int villagersAmount)
    {
        for (int i = 0; i < villagersAmount; i++)
        {
            int rand = Random.Range(0, villagersAmount - i);
            createdVillagersRoles.Add(villagersRoles[rand]);
            villagersRoles.RemoveAt(rand);
        }
    }

    public void CreateOutcasts(int outcastsAmount)
    {
        for (int i = 0; i < outcastsAmount; i++)
        {
            int rand = Random.Range(0, outcastsAmount - i);
            createdOutcastsRoles.Add(outcastsRoles[rand]);
            outcastsRoles.RemoveAt(rand);
        }
    }

    public void CreateEvils(int evilsAmount)
    {
        for (int i = 0; i < this.evilsAmount; i++)
        {
            int rand = Random.Range(0, evilsAmount - i);
            createdEvilsRoles.Add(evilsRoles[rand]);
            evilsRoles.RemoveAt(rand);
        }
    }

    public void ImplimentEvil()
    {
        for (int i = 0; i < createdEvilsRoles.Count; i++)
        {
            Role role = Instantiate(mergedList[i]);
            mergedList[i] = Instantiate(createdEvilsRoles[i]);
            mergedList[i]._substituteRole = role;
        }
    }
    
    public void ClearRoles()
    {
        for (int i = 0; i < createdVillagersRoles.Count; i++)
        {
            villagersRoles.Add(createdVillagersRoles[i]);
        }
        createdVillagersRoles.Clear();
        villagersCards.Clear();
        
        for (int i = 0; i < createdOutcastsRoles.Count; i++)
        {
            outcastsRoles.Add(createdOutcastsRoles[i]);
        }
        createdOutcastsRoles.Clear();
        outcastsCards.Clear();
        
        for (int i = 0; i < createdEvilsRoles.Count; i++)
        {
            evilsRoles.Add(createdEvilsRoles[i]);
        }
        createdEvilsRoles.Clear();
        evilsCards.Clear();
    }
}
