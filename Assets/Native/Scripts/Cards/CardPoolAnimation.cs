using System;
using DG.Tweening;
using UnityEngine;

public class CardPoolAnimation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveDuration = 0.4f; 
    [SerializeField] private float moveDelayMultiply = 0.2f;
    
    [Header("Dependence")]
    [SerializeField] private Card card;
    [SerializeField] private CardFlipAnimation cardFlipAnimation;
    [SerializeField] private Collider2D cardCollider;
    
    private Vector3 startPosition;

    private void OnEnable()
    {
        EventBus.RestartRound += ShuffleAnimation;
    }

    private void OnDisable()
    {
        EventBus.RestartRound -= ShuffleAnimation;
    }

    private void Start()
    {
        ShuffleAnimation();
    }

    public void ShuffleAnimation()
    {
        startPosition = new Vector3(0f, 0f, 0f);
        var targetPos = new Vector3(0f, 6f, 0f);
        transform.localPosition = startPosition;
        Sequence cardsMoveSequence = DOTween.Sequence();
        cardsMoveSequence
            .Append(transform.DOLocalMove(targetPos, moveDuration).SetDelay(card.cardId * moveDelayMultiply).From(startPosition).SetEase(Ease.InOutSine));
        cardsMoveSequence.OnPlay(() =>
        {
            cardFlipAnimation.cardCollider.enabled = false;
            cardCollider.enabled = false;
            card._cardNumber.enabled = false;
            
        });
        cardsMoveSequence.OnComplete(() => {
            cardFlipAnimation.cardCollider.enabled = true;
            cardCollider.enabled = true;
            card._cardNumber.enabled = true;
        });
    }
}
