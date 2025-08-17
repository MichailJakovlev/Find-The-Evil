using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Transforms")]
    [SerializeField] private Transform cardFront;
    [SerializeField] private Transform cardBack;
    [SerializeField] private Transform cardStrokeFront;
    [SerializeField] private Transform cardStrokeBack;
    
    [Header("Collider")]
    public BoxCollider2D cardCollider;
    [SerializeField] private Card card;
    
    [Header("Settings")]
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private float OnPointerEnterMoveY = 0.4f;
    [SerializeField] private float OnPointerExitScaleDuration = 0.3f;
    
    public SpriteRenderer cardBackSpriteRenderer;
    public SpriteRenderer cardImageSpriteRenderer;
    public SpriteRenderer cardStrokeFrontSpriteRenderer;
    public SpriteRenderer cardStrokeBackSpriteRenderer;
    private Vector3 startPosition;
    
    private bool isFlipped = false;
    public bool isKillMode = false;

    public void OnEnable()
    {
        EventBus.KillModeOn += CardKillModeOn;
        EventBus.KillModeOff += CardKillModeOff;
    }

    public void OnDisable() 
    {
        EventBus.KillModeOn -= CardKillModeOn;
        EventBus.KillModeOff -= CardKillModeOff;
    }
    
    public void CardKillModeOn()
    {
        isKillMode = true;
    }

    public void CardKillModeOff()
    {
        isKillMode = false;
    }
    
    private void Start()
    {
        startPosition = cardBack.position;
        cardBackSpriteRenderer = cardBack.GetComponent<SpriteRenderer>();
        cardImageSpriteRenderer = cardFront.GetComponent<SpriteRenderer>();
        cardStrokeFrontSpriteRenderer = cardStrokeFront.GetComponent<SpriteRenderer>();
        cardStrokeBackSpriteRenderer = cardStrokeBack.GetComponent<SpriteRenderer>();
        card._cardName.gameObject.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), animationDuration).From(new Vector3(1.1f, 1.1f, 1.1f));
        
        cardBack.DOMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        cardFront.DOMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        cardStrokeFront.DOMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        cardStrokeBack.DOMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        
        if (!isFlipped)
        {
            // cardStrokeBackSpriteRenderer.DOFade(1f, animationDuration);
            cardStrokeBackSpriteRenderer.enabled = true;
        }
        else
        {
            cardStrokeFrontSpriteRenderer.enabled = true;
            // cardStrokeFrontSpriteRenderer.DOFade(1f, animationDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), OnPointerExitScaleDuration);
        
        cardBack.DOMove(startPosition, animationDuration);
        cardFront.DOMove(startPosition, animationDuration);
        cardStrokeFront.DOMove(startPosition, animationDuration);
        cardStrokeBack.DOMove(startPosition, animationDuration);
       
        
        if (!isFlipped)
        {
            // cardStrokeBackSpriteRenderer.DOFade(0f, animationDuration);
            cardStrokeBackSpriteRenderer.enabled = false;
        }
        else
        {
            // cardStrokeFrontSpriteRenderer.DOFade(0f, animationDuration);
            cardStrokeFrontSpriteRenderer.enabled = false;
        }
    }
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            // Debug.Log(name + " Game Object Right Clicked!" + pointerEventData);
        }
        
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (!isFlipped)
            {
                Sequence flipCardSequence = DOTween.Sequence();
                flipCardSequence
                    .Append(cardBack.DORotate(new Vector3(0f, -90f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
                    .Join(cardStrokeBack.DORotate(new Vector3(0f, -90f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
                    .Append(cardFront.DORotate(new Vector3(0f, 0f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 90f, 0f)))
                    .Join(cardStrokeFront.DORotate(new Vector3(0f, 0f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 90f, 0f)))
                    .Join(cardBackSpriteRenderer.DOFade(0f, 0f))
                    .Join(cardImageSpriteRenderer.DOFade(1f, 0f));
                
                cardStrokeBackSpriteRenderer.enabled = false;
                
                flipCardSequence.OnPlay(() =>
                {
                    cardCollider.enabled = false;
                });
                flipCardSequence.OnComplete(() => {
                    cardCollider.enabled = true;
                   
                });
                isFlipped = true;
               // Debug.Log(name + " Game Object Left Clicked!");
               card.ShowMessage();
               card._cardName.gameObject.SetActive(true);
            }
            
            // else if (isFlipped)
            // { 
            //     Sequence flipCardSequence = DOTween.Sequence();
            //     flipCardSequence
            //         .Append(cardFront.DORotate(new Vector3(0f, 90f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
            //         .Join(cardStrokeFront.DORotate(new Vector3(0f, 90f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
            //         .Append(cardBack.DORotate(new Vector3(0f, 0f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            //         .Join(cardStrokeBack.DORotate(new Vector3(0f, 0f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            //         .Join(cardBackSpriteRenderer.DOFade(1f, 0f))
            //         .Join(cardImageSpriteRenderer.DOFade(0f, 0f));
            //     
            //     cardStrokeFrontSpriteRenderer.enabled = false;
            //     
            //     flipCardSequence.OnPlay(() =>
            //     {
            //         cardCollider.enabled = false;
            //     });
            //     flipCardSequence.OnComplete(() => {
            //         cardCollider.enabled = true;
            //     });
            //     isFlipped = false;
            //   //  Debug.Log(name + " Game Object Left Clicked!");
            // }
            if (isKillMode)
            {
                card.KillCard();
            }
        }
    }
}
