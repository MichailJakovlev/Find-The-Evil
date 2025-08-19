using UnityEngine;
using DG.Tweening;

public class CardFlipAnimation : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform cardFront;
    [SerializeField] private Transform cardBack;
    [SerializeField] private Transform cardStrokeFront;
    [SerializeField] private Transform cardStrokeBack;
    [SerializeField] private GameObject cardItem;
    
    [Header("Object Links")]
    [SerializeField] private Card card;
    [SerializeField] private CardHandler cardHandler;
    
    [Header("Collider")]
    public BoxCollider2D cardCollider;

    [Header("Settings")]
    [SerializeField] private float animationDuration = 0.1f;
    [SerializeField] private float OnPointerEnterMoveY = 0.4f;
    [SerializeField] private float OnPointerExitScaleDuration = 0.3f;
     
    [HideInInspector]
    public SpriteRenderer cardBackSpriteRenderer;
    [HideInInspector]
    public SpriteRenderer cardImageSpriteRenderer;
    [HideInInspector]
    public SpriteRenderer cardStrokeFrontSpriteRenderer;
    [HideInInspector]
    public SpriteRenderer cardStrokeBackSpriteRenderer;
    
    private bool isFlipped = false;
    private Vector3 startPosition;    
    
    private void Start()
    {
        startPosition = cardBack.localPosition;
        cardBackSpriteRenderer = cardBack.GetComponent<SpriteRenderer>();
        cardImageSpriteRenderer = cardFront.GetComponent<SpriteRenderer>();
        cardStrokeFrontSpriteRenderer = cardStrokeFront.GetComponent<SpriteRenderer>();
        cardStrokeBackSpriteRenderer = cardStrokeBack.GetComponent<SpriteRenderer>();
    }
        
    public void OnCardEnter() 
    {
        transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), animationDuration).From(new Vector3(1.1f, 1.1f, 1.1f));
                    
        cardBack.DOLocalMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        cardFront.DOLocalMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        cardStrokeFront.DOLocalMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
        cardStrokeBack.DOLocalMove(startPosition + new Vector3(0f, OnPointerEnterMoveY, 0f), animationDuration).From(startPosition);
                    
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
    
    public void OnCardExit()
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), OnPointerExitScaleDuration);
                
        cardBack.DOLocalMove(startPosition, animationDuration);
        cardFront.DOLocalMove(startPosition, animationDuration);
        cardStrokeFront.DOLocalMove(startPosition, animationDuration);
        cardStrokeBack.DOLocalMove(startPosition, animationDuration);
                       
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
    
    public void OnCardClick()
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
            cardHandler.isFlipped = true;
        }
    }
    
    public void OnCardClose()
    {
        Sequence flipCardSequence = DOTween.Sequence();
        flipCardSequence
            .Append(cardFront.DORotate(new Vector3(0f, 90f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
            .Join(cardStrokeFront.DORotate(new Vector3(0f, 90f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
            .Append(cardBack.DORotate(new Vector3(0f, 0f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            .Join(cardStrokeBack.DORotate(new Vector3(0f, 0f, 0f), animationDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            .Join(cardImageSpriteRenderer.DOFade(0f, 0f))
            .Join(cardBackSpriteRenderer.DOFade(1f, 0f));
                    
        cardStrokeFrontSpriteRenderer.enabled = false;
                    
        flipCardSequence.OnPlay(() =>
        {
            cardCollider.enabled = false;
        });
        isFlipped = false;
        cardHandler.isFlipped = false;
    }
}
