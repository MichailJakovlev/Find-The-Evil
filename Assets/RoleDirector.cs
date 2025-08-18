using System.Collections.Generic;
using UnityEngine;

public class RoleDirector : MonoBehaviour
{
    [SerializeField] private List<Role> roles;
    public CardPool cardPool;
    public List<Card> villagers;
    public List<Card> outcasts;
    public List<Card> evils;
    
    public void AssignRoles()
    {
        for (int i = 0; i < cardPool._cardAmount; i++)
        {
            cardPool.cards[i]._cardRole = Instantiate(roles[Random.Range(0, roles.Count)]);
            
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
            cardPool.cards[i]._cardRole._cardNumber = (cardPool._cardAmount - i);
        }
    }
}
