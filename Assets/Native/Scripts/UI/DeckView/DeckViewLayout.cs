using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeckViewLayout : MonoBehaviour
{
    [SerializeField] private GameObject deckViewItemPrefab;
    [SerializeField] private DeckViewItem deckViewItem;
    
    private RoleDirector roleDirector;
    private List<Role> deckViewCardList;

    private void OnEnable()
    {
        roleDirector = GameObject.Find("Role Director").GetComponent<RoleDirector>();
        deckViewCardList = roleDirector.createdVillagersRoles.Concat(roleDirector.createdOutcastsRoles).ToList().Concat(roleDirector.createdEvilsRoles).ToList();
        foreach (Role role in deckViewCardList)
        {
            deckViewItem.roleNameText.text = role._cardName;
            var instance = Instantiate(deckViewItemPrefab, transform);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
