using System;
using DG.Tweening;
using UnityEngine;

public class CardPollAnimation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveDuration = 0.4f; 
    [SerializeField] private float moveDelayMultiply = 0.2f;
    
    [Header("Dependence")]
    [SerializeField] private Card card;
    [SerializeField] private CardHandler cardHandler;
    
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = new Vector3(0f, 0f, 0f);
        var targetPos = new Vector3(0f, 6f, 0f);
        transform.localPosition = startPosition;
        Sequence cardsMoveSequence = DOTween.Sequence();
        cardsMoveSequence
            .Append(transform.DOLocalMove(targetPos, moveDuration).SetDelay(card.cardId * moveDelayMultiply).From(startPosition).SetEase(Ease.InOutSine));
        cardsMoveSequence.OnPlay(() =>
        {
            cardHandler.cardCollider.enabled = false;
            
        });
        cardsMoveSequence.OnComplete(() => {
            cardHandler.cardCollider.enabled = true;
        });
    }
}
