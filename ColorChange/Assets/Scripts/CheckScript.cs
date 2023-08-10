using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckScript : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject tabloPanel;

    public bool Check(GameObject hitObject1, GameObject hitObject2)
    {
        bool checkResult = false;

        ImagesScript imagesScript1 = hitObject1.GetComponent<ImagesScript>();
        ImagesScript imagesScript2 = hitObject2.GetComponent<ImagesScript>();

        for (int i = 0; i < imagesScript1.items.Count; i++)
        {
            if (imagesScript1.items[i] == hitObject2.gameObject)
            {
                checkResult = true;
                break;
            }
        }

        if (checkResult)
        {
            CheckFinish();
            hitObject2.GetComponent<ImagesScript>().mainColor = hitObject1.GetComponent<Image>().color;
            return true;
        }
        else
        {
            SetOutlineColor(hitObject1, Color.red);
            SetOutlineColor(hitObject2, Color.red);
            return false;
        }
    }

    public void Change(GameObject hitObject1, GameObject hitObject2)
    {
        Image imageComponent1 = hitObject1.GetComponent<Image>();
        Image imageComponent2 = hitObject2.GetComponent<Image>();

        imageComponent2.DOColor(imageComponent1.color, 0.5f)
            .OnComplete(() => GetComponent<ButtonsScript>().ChangeCheck());

        Vector2 originalScale = hitObject2.transform.localScale;
        Vector2 modifiedScale = originalScale + new Vector2(0.2f, 0.2f);

        hitObject2.transform.DOScale(modifiedScale, 0.5f)
            .OnComplete(() => hitObject2.transform.DOScale(originalScale, 0.3f));

        GetComponent<ColorChange>().MakeStart();
    }

    private void CheckFinish()
    {
        StartCoroutine(WaitForCheck());
    }

    private IEnumerator WaitForCheck()
    {
        yield return new WaitForSeconds(0.4f);

        bool checkFinished = true;

        for (int i = 0; i < mainPanel.transform.childCount; i++)
        {
            Image mainImage = mainPanel.transform.GetChild(i).GetComponent<Image>();
            Image tabloImage = tabloPanel.transform.GetChild(i).GetComponent<Image>();

            if (mainImage.color != tabloImage.color)
            {
                checkFinished = false;
                break;
            }
        }

        if (checkFinished)
        {
            GetComponent<StartScript>().FinishAnimation();
        }
    }

    private void SetOutlineColor(GameObject obj, Color color)
    {
        Outline outlineComponent = obj.GetComponent<Outline>();
        outlineComponent.effectColor = color;
        outlineComponent.DOColor(Color.black, 0.5f);
    }
}