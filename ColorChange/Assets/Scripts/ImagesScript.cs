using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesScript : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private Sequence colorAnimationSequence;
    public Color mainColor;

    private void Start()
    {
        InitializeColliderSize();
        InitializeMainColor();
    }

    private void InitializeColliderSize()
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        BoxCollider2D boxCollider2D = transform.GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(rectTransform.rect.size.x + 20, rectTransform.rect.size.y + 20);
    }

    private void InitializeMainColor()
    {
        mainColor = transform.GetComponent<Image>().color;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        ImagesScript otherImagesScript = other.transform.GetComponent<ImagesScript>();

        if (otherImagesScript != null && !items.Contains(other.gameObject))
        {
            items.Add(other.gameObject);
        }
    }

    public void ColorAnim(GameObject hitObject)
    {
        colorAnimationSequence = DOTween.Sequence();
        Image imageComponent = transform.GetComponent<Image>();

        colorAnimationSequence
            .Append(imageComponent.DOColor(hitObject.GetComponent<Image>().color, 0.5f))
            .Append(imageComponent.DOColor(mainColor, 0.5f))
            .Append(imageComponent.DOColor(mainColor, 2f))
            .SetLoops(10);
    }

    public void ColorAnimKill()
    {
        Image imageComponent = transform.GetComponent<Image>();
        imageComponent.color = mainColor;

        if (colorAnimationSequence != null)
        {
            colorAnimationSequence.Kill();
        }
    }
}