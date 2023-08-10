using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSceneScript : MonoBehaviour
{
    public Text levelText;
    public GameObject button;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private Vector2 levelTextOldPos, levelTextNewPos;
    private Vector2 buttonOldPos, buttonNewPos;
    private Vector2 starsOldScale;
    private float animationRange = 0.5f;

    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("current_level");
        levelText.text = "Level " + currentLevel;

        InitializePositionsAndScales();
        StartAnimation();
    }

    private void InitializePositionsAndScales()
    {
        levelTextOldPos = levelText.transform.position;
        levelText.transform.position = new Vector2(levelText.transform.position.x, levelText.transform.position.y + 10);
        levelTextNewPos = levelText.transform.position;

        buttonOldPos = button.transform.position;
        button.transform.position = new Vector2(button.transform.position.x, button.transform.position.y - 10);
        buttonNewPos = button.transform.position;

        starsOldScale = star1.transform.localScale;
        star1.transform.localScale = Vector2.zero;
        star2.transform.localScale = Vector2.zero;
        star3.transform.localScale = Vector2.zero;
    }

    private void StartAnimation()
    {
        levelText.transform.DOMove(levelTextOldPos, animationRange + 0.2f).SetEase(Ease.OutBack).OnComplete(() => StarsAnimation());
    }

    private void StarsAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        Tween tweenStar1 = star1.transform.DOScale(starsOldScale, animationRange).SetEase(Ease.OutBack);
        Tween tweenStar2 = star2.transform.DOScale(starsOldScale, animationRange).SetEase(Ease.OutBack);
        Tween tweenStar3 = star3.transform.DOScale(starsOldScale, animationRange).SetEase(Ease.OutBack);
        Tween tweenButton = button.transform.DOMove(buttonOldPos, animationRange + 0.2f).SetEase(Ease.OutBack);

        sequence.Append(tweenStar1).Append(tweenStar2).Append(tweenStar3).Append(tweenButton);
    }

    public void SkipButton()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(levelText.transform.DOMove(levelTextNewPos, animationRange + 0.2f).SetEase(Ease.InBack));

        int currentLevel = PlayerPrefs.GetInt("current_level");
        string nextLevelName = currentLevel != 5 ? (currentLevel + 1).ToString() : "1";
        sequence.Append(button.transform.DOMove(buttonNewPos, animationRange + 0.2f).SetEase(Ease.InBack).OnComplete(() => Application.LoadLevel(nextLevelName)));

        sequence.Join(star1.transform.DOScale(Vector2.zero, animationRange).SetEase(Ease.InBack));
        sequence.Join(star2.transform.DOScale(Vector2.zero, animationRange).SetEase(Ease.InBack));
        sequence.Join(star3.transform.DOScale(Vector2.zero, animationRange).SetEase(Ease.InBack));
    }
}