using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoleDirector : MonoBehaviour
{
    [Header("Delete This")] // Временное поле
    [SerializeField] private List<Role> roles;
    
    [Header("Roles")]
    [SerializeField] private List<Role> villagersRoles;
    [SerializeField] private List<Role> outcastsRoles;
    [SerializeField] private List<Role> evilsRoles;
    
    [Header("Object Links")]
    [SerializeField] private CardPool cardPool;
    
    [Header("Created Roles")]
    public List<Card> villagers;
    public List<Card> outcasts;
    public List<Card> evils;

    private AbilityInfo _abilityInfo;
    
    public void AssignRoles()
    {
        _abilityInfo = FindObjectOfType<AbilityInfo>();
        for (int i = 0; i < cardPool._cardAmount; i++)
        {
           cardPool.cards[i]._cardRole = Instantiate(roles[Random.Range(0, roles.Count)], cardPool.cards[i].transform.GetChild(0).transform);
            
            if (cardPool.cards[i]._cardRole._roleType == "Evil")
            {
                evils.Add(cardPool.cards[i]);

            }
            else if(cardPool.cards[i]._cardRole._roleType == "Outcast")
            {
                outcasts.Add(cardPool.cards[i]);
            }
            else
            {
                villagers.Add(cardPool.cards[i]);
            }
        }
        
        for (int i = 0; i < evils.Count; i++)
        {
            evils[i]._cardRole._substituteRole = villagers[Random.Range(0, villagers.Count)]._cardRole;
        }
        
        for (int i = 0; i < cardPool._cardAmount; i++)
        {
            cardPool.cards[i].Init();
            cardPool.cards[i]._cardRole._roleDirector = this;
            cardPool.cards[i]._cardRole._abilityInfo = _abilityInfo;
            cardPool.cards[i]._cardRole._cardNumber = (cardPool._cardAmount - i);
        }
    }

    public void CreateEvils(int evilsAmount)
    {
        for (int i = 0; i < evilsAmount; i++)
        {
            cardPool.cards[i]._cardRole = Instantiate(evilsRoles[Random.Range(0, evilsRoles.Count)], cardPool.cards[i].transform.GetChild(0).transform);
        }
    }

    public void CreateOutcasts(int outcastsAmount)
    {
        
    }

    public void CreateVillagers(int villagersAmount)
    {
        
    }
    
    public void ClearRoles()
    {
        // for (int i = 0; i < villagers.Count; i++)
        // {
        //     Destroy(villagers[i]);
        // }
            
        villagers.Clear();
        
        // for (int i = 0; i < outcasts.Count; i++)
        // {
        //     Destroy(outcasts[i]);
        // }
        
        outcasts.Clear();
        
        // for (int i = 0; i < evils.Count; i++)
        // {
        //     Destroy(evils[i]);
        // }
       
        evils.Clear();
    }
}
