using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public GameObject main;
    private Vector2[] oldPositions;
    private Vector2[] newPositions;
    private float imageRange = 10f;
    private float startAnimRange = 0.6f;
    public GameObject buttonsTop;
    private Vector2 buttonsTopOldPos;
    public GameObject paint;
    private Vector2 paintOldPos;

    private void Start()
    {
        InitializePlayerPrefs();

        oldPositions = new Vector2[main.transform.childCount];
        newPositions = new Vector2[main.transform.childCount];

        for (int i = 0; i < oldPositions.Length; i++)
        {
            oldPositions[i] = main.transform.GetChild(i).transform.position;
        }

        ShuffleImagePositions();
        SetNewPositions();
        InitializeButtonsAndPaintPositions();
        StartCoroutine(AnimateImages());
    }

    private void InitializePlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("current_level"))
        {
            PlayerPrefs.SetInt("current_level", 1);
        }
    }

    private void ShuffleImagePositions()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            int rand = Random.Range(1, 5);
            Vector2 offset = Vector2.zero;

            if (rand == 1) offset = new Vector2(imageRange, 0);
            else if (rand == 2) offset = new Vector2(-imageRange, 0);
            else if (rand == 3) offset = new Vector2(0, imageRange);
            else if (rand == 4) offset = new Vector2(0, -imageRange);

            main.transform.GetChild(i).transform.position += offset;
        }
    }

    private void SetNewPositions()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            newPositions[i] = main.transform.GetChild(i).transform.position;
        }
    }

    private void InitializeButtonsAndPaintPositions()
    {
        buttonsTopOldPos = buttonsTop.transform.position;
        buttonsTop.transform.position -= new Vector2(imageRange, 0);

        paintOldPos = paint.transform.position;
        paint.transform.position += new Vector2(imageRange, 0);
    }

    private IEnumerator AnimateImages()
    {
        yield return new WaitForSeconds(0.5f);
        AnimateStart();
    }

    private void AnimateStart()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            Image imageComponent = main.transform.GetChild(i).GetComponent<Image>();
            Color mainColor = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, imageComponent.color.a);
            Vector2 oldScale = main.transform.GetChild(i).transform.localScale;

            imageComponent.color = new Color(0, 0, 0, 0);
            main.transform.GetChild(i).localScale = Vector2.zero;

            main.transform.GetChild(i).transform.DOMove(oldPositions[i], startAnimRange).SetEase(Ease.OutBack).OnComplete(() => EnableImageColliders());
            imageComponent.DOColor(mainColor, startAnimRange);
            main.transform.GetChild(i).transform.DOScale(oldScale, startAnimRange);
        }

        StartCoroutine(WaitBeforeAnimatingButtonsAndPaint());
    }

    private void EnableImageColliders()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private IEnumerator WaitBeforeAnimatingButtonsAndPaint()
    {
        yield return new WaitForSeconds(startAnimRange);
        AnimateButtonsAndPaint();
    }

    private void AnimateButtonsAndPaint()
    {
        Sequence buttonsAndPaintAnimation = DOTween.Sequence();
        buttonsAndPaintAnimation.Append(buttonsTop.transform.DOMove(buttonsTopOldPos, startAnimRange).SetEase(Ease.OutBack));
        buttonsAndPaintAnimation.Join(paint.transform.DOMove(paintOldPos, startAnimRange).SetEase(Ease.OutBack));
    }

    public void RestartAnimation()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }

        Sequence restartAnimation = DOTween.Sequence();
        for (int i = 0; i < main.transform.childCount; i++)
        {
            restartAnimation.Append(main.transform.GetChild(i).transform.DOMove(newPositions[i], startAnimRange).SetEase(Ease.InBack));
            restartAnimation.Join(main.transform.GetChild(i).GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), startAnimRange));
            restartAnimation.Join(main.transform.GetChild(i).transform.DOScale(Vector2.zero, startAnimRange));
        }

        restartAnimation.Append(buttonsTop.transform.DOMove(new Vector2(buttonsTop.transform.position.x - imageRange, buttonsTop.transform.position.y), startAnimRange).SetEase(Ease.InBack));
        restartAnimation.Join(paint.transform.DOMove(new Vector2(paint.transform.position.x + imageRange, paint.transform.position.y), startAnimRange).SetEase(Ease.InBack));

        restartAnimation.OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    public void FinishAnimation()
    {
        int temp = int.Parse(SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("current_level", temp);
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }

        Sequence finishAnimation = DOTween.Sequence();
        for (int i = 0; i < main.transform.childCount; i++)
        {
            finishAnimation.Append(main.transform.GetChild(i).transform.DOMove(newPositions[i], startAnimRange).SetEase(Ease.InBack));
            finishAnimation.Join(main.transform.GetChild(i).GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), startAnimRange));
            finishAnimation.Join(main.transform.GetChild(i).transform.DOScale(Vector2.zero, startAnimRange));
        }

        finishAnimation.Append(buttonsTop.transform.DOMove(new Vector2(buttonsTop.transform.position.x - imageRange, buttonsTop.transform.position.y), startAnimRange).SetEase(Ease.InBack));
        finishAnimation.Join(paint.transform.DOMove(new Vector2(paint.transform.position.x + imageRange, paint.transform.position.y), startAnimRange).SetEase(Ease.InBack));

        finishAnimation.OnComplete(() => SceneManager.LoadScene("level_scane"));
    }
}