using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Color[] backColors;
    public Color[] frontColors;
    public Color[] tempColors;
    public GameObject mainPanel;
    public GameObject backButtonObject;
    private int changeObjectNumber = 20;

    private void Start()
    {
        InitializeColors();
    }

    private void InitializeColors()
    {
        backColors = new Color[mainPanel.transform.childCount];
        frontColors = new Color[mainPanel.transform.childCount];
        tempColors = new Color[mainPanel.transform.childCount];

        for (int i = 0; i < frontColors.Length; i++)
        {
            Image imageComponent = mainPanel.transform.GetChild(i).GetComponent<Image>();
            frontColors[i] = imageComponent.color;
            tempColors[i] = imageComponent.color;
            backColors[i] = new Color(0, 0, 0, 255);
        }
    }

    public void RestartButton()
    {
        GetComponent<StartScript>().RestartAnimation();
    }

    public void BackButton()
    {
        if (backColors[0] != new Color(0, 0, 0, 255))
        {
            for (int i = 0; i < backColors.Length; i++)
            {
                Image imageComponent = mainPanel.transform.GetChild(i).GetComponent<Image>();
                imageComponent.color = backColors[i];
                imageComponent.GetComponent<ImagesScript>().mainColor = imageComponent.color;
            }

            Sequence seq = DOTween.Sequence();
            Transform changeObject = mainPanel.transform.GetChild(changeObjectNumber).transform;
            Vector2 oldScale = changeObject.localScale;

            Tween tween = changeObject.DOScale(new Vector2(oldScale.x + 0.2f, oldScale.y + 0.2f), 0.2f);
            Tween tween2 = changeObject.DOScale(oldScale, 0.2f);

            seq.Append(tween).Append(tween2);
            BackButtonAnimation();
        }
    }

    private void BackButtonAnimation()
    {
        Destroy(backButtonObject.transform.GetChild(0).gameObject);
        backButtonObject.transform.DOMove(new Vector2(backButtonObject.transform.position.x - 10, backButtonObject.transform.position.y), 0.7f).SetEase(Ease.InBack);
    }

    public void CheckForChange()
    {
        bool isSame = true;

        for (int i = 0; i < frontColors.Length; i++)
        {
            frontColors[i] = mainPanel.transform.GetChild(i).GetComponent<Image>().color;
        }

        for (int i = 0; i < frontColors.Length; i++)
        {
            if (frontColors[i] != tempColors[i])
            {
                isSame = false;
                changeObjectNumber = i;
                break;
            }
        }

        if (!isSame)
        {
            for (int i = 0; i < frontColors.Length; i++)
            {
                backColors[i] = tempColors[i];
                tempColors[i] = frontColors[i];
            }
        }
    }
}