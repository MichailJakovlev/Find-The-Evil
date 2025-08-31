using UnityEngine;
using DG.Tweening;

public class CardFlipAnimation : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform cardFront;
    [SerializeField] private Transform cardBack;
    [SerializeField] private Transform cardStrokeFront;
    [SerializeField] private Transform cardStrokeBack;
    [SerializeField] private Transform cardShadow;
    [SerializeField] private GameObject cardItem;
    
    [Header("Object Links")]
    [SerializeField] private Card card;
    [SerializeField] private CardHandler cardHandler;
    
    [Header("Collider")]
    public BoxCollider2D cardCollider;

    [Header("Durations")]
    [SerializeField] private float flipDuration = 0.1f;
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float onPointerEnterScaleDuration = 0.1f;
    [SerializeField] private float onPointerExitScaleDuration = 0.3f;
    
    [Header("Scales")]
    [SerializeField] private float cardPointerEnterScale = 1.2f;
    [SerializeField] private float shadowPointerEnterScale = 1.1f;
    
    [Header("Move Distance")]
    [SerializeField] private float OnPointerEnterMoveY = 0.4f;
     
    [HideInInspector]
    public SpriteRenderer cardBackSpriteRenderer;
    [HideInInspector]
    public SpriteRenderer cardImageSpriteRenderer;
    [HideInInspector]
    public SpriteRenderer cardStrokeFrontSpriteRenderer;
    [HideInInspector]
    public SpriteRenderer cardStrokeBackSpriteRenderer;

    private Sequence hoverAnimationSequence;
    
    private bool isFlipped = false;
    private Vector3 cardStartPosition;
    
    private void Start()
    {
        cardStartPosition = cardBack.localPosition;
        cardBackSpriteRenderer = cardBack.GetComponent<SpriteRenderer>();
        cardImageSpriteRenderer = cardFront.GetComponent<SpriteRenderer>();
        cardStrokeFrontSpriteRenderer = cardStrokeFront.GetComponent<SpriteRenderer>();
        cardStrokeBackSpriteRenderer = cardStrokeBack.GetComponent<SpriteRenderer>();
    }
        
    public void OnCardEnter()
    {
        KillCurrentAnimationSequence();
        
        hoverAnimationSequence = DOTween.Sequence();
        hoverAnimationSequence
            .Append(transform.DOScale(new Vector3(cardPointerEnterScale, cardPointerEnterScale, cardPointerEnterScale), onPointerEnterScaleDuration).From(new Vector3(1.1f, 1.1f, 1.1f)))
            .Join(cardShadow.DOScale(new Vector3(shadowPointerEnterScale, shadowPointerEnterScale, shadowPointerEnterScale), onPointerEnterScaleDuration).From(new Vector3(1f, 1f, 1f)))
            .Join(cardBack.DOLocalMove(new Vector3(0f, OnPointerEnterMoveY, 0f), moveDuration).From(cardStartPosition))
            .Join(cardFront.DOLocalMove(new Vector3(0f, OnPointerEnterMoveY, 0f), moveDuration).From(cardStartPosition))
            .Join(cardStrokeFront.DOLocalMove(new Vector3(0f, OnPointerEnterMoveY, 0f), moveDuration).From(cardStartPosition))
            .Join(cardStrokeBack.DOLocalMove(new Vector3(0f, OnPointerEnterMoveY, 0f), moveDuration).From(cardStartPosition));
                    
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
        KillCurrentAnimationSequence();
        
        hoverAnimationSequence = DOTween.Sequence();
        hoverAnimationSequence
            .Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), onPointerExitScaleDuration))
            .Join(cardShadow.DOScale(new Vector3(1f, 1f, 1f), onPointerExitScaleDuration))
            .Join(cardBack.DOLocalMove(cardStartPosition, moveDuration))
            .Join(cardFront.DOLocalMove(cardStartPosition, moveDuration))
            .Join(cardStrokeFront.DOLocalMove(cardStartPosition, moveDuration))
            .Join(cardStrokeBack.DOLocalMove(cardStartPosition, moveDuration));
        
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
                .Append(cardBack.DORotate(new Vector3(0f, -90f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
                .Join(cardStrokeBack.DORotate(new Vector3(0f, -90f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
                .Join(cardShadow.DORotate(new Vector3(0f, -90f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
                .Append(cardFront.DORotate(new Vector3(0f, 0f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 90f, 0f)))
                .Join(cardStrokeFront.DORotate(new Vector3(0f, 0f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 90f, 0f)))
                .Join(cardShadow.DORotate(new Vector3(0f, 0f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
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
            .Append(cardFront.DORotate(new Vector3(0f, 90f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
            .Join(cardStrokeFront.DORotate(new Vector3(0f, 90f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
            .Join(cardShadow.DORotate(new Vector3(0f, 0f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            .Append(cardBack.DORotate(new Vector3(0f, 0f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            .Join(cardStrokeBack.DORotate(new Vector3(0f, 0f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, -90f, 0f)))
            .Join(cardShadow.DORotate(new Vector3(0f, -90f, 0f), flipDuration, RotateMode.Fast).From(new Vector3(0f, 0f, 0f)))
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
    
    private bool IsHoverAnimationSequence() => hoverAnimationSequence != null && hoverAnimationSequence.active;

    private void KillCurrentAnimationSequence()
    {
        if (IsHoverAnimationSequence())
        {
            hoverAnimationSequence.Kill();
        }
    }
}
