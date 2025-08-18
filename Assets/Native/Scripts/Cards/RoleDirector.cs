using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoleDirector : MonoBehaviour
{
    [SerializeField] private List<Role> roles;
    public CardPool cardPool;
    public List<Card> villagers;
    public List<Card> outcasts;
    public List<Card> evils;

    private AbilityInfo _abilityInfo;
    
    public void AssignRoles()
    {
        _abilityInfo = FindObjectOfType<AbilityInfo>();
        for (int i = 0; i < cardPool._cardAmount; i++)
        {
            cardPool.cards[i]._cardRole = Instantiate(roles[Random.Range(0, roles.Count)], transform);
            
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

    public void ClearRoles()
    {
        villagers.Clear();
        outcasts.Clear();
        evils.Clear();
    }
}
