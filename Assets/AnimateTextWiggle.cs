using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimateTextWiggle : MonoBehaviour
{
    // Make the UI object wiggle
    [SerializeField] private Vector2 wiggleAmt = new Vector2(10f, 0f); // Default values for wiggle amount
    [SerializeField] private float wiggleSpeed = 0.5f; // Adjusted wiggle speed
    
    [SerializeField] private float growAmt = 1.2f; // Default value for grow amount
    [SerializeField] private float growSpeed = 0.5f; // Adjusted grow speed
    
    // Get UI object
    private RectTransform rectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component not found on this GameObject.");
            return;
        }
        
        // TweenWiggleForever();
        TweenSizeForever();
    }

    void TweenWiggleForever()
    {
        DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(new Vector2(wiggleAmt.x, wiggleAmt.y), wiggleSpeed).SetEase(Ease.InOutSine))
            .Append(rectTransform.DOAnchorPos(new Vector2(-wiggleAmt.x, -wiggleAmt.y), wiggleSpeed).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo); // Changed to Yoyo loop type for smoother transition
    }

    void TweenSizeForever()
    {
        Vector3 initialScale = rectTransform.localScale;
        DOTween.Sequence()
            .Append(rectTransform.DOScale(new Vector3(initialScale.x * growAmt, initialScale.y * growAmt, initialScale.z), growSpeed).SetEase(Ease.InOutSine))
            .Append(rectTransform.DOScale(initialScale, growSpeed).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo); // Changed to Yoyo loop type for smoother transition
    }
}
