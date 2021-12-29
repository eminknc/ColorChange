using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_scane_script : MonoBehaviour
{
    public Text level_text;
    public GameObject button;
    public GameObject star_1;
    public GameObject star_2;
    public GameObject star_3;
    Vector2 level_text_old_pos, level_text_new_pos;
    Vector2 button_old_pos, button_new_pos;
    Vector2 stars_old_scale;
    float anim_range = 0.5f;

    void Start()
    {
        level_text.text = "Level " + PlayerPrefs.GetInt("current_level");

        level_text_old_pos = level_text.transform.position;
        level_text.transform.position = new Vector2(level_text.transform.position.x, level_text.transform.position.y + 10);
        level_text_new_pos = level_text.transform.position;

        button_old_pos = button.transform.position;
        button.transform.position = new Vector2(button.transform.position.x, button.transform.position.y - 10);
        button_new_pos = button.transform.position;

        stars_old_scale = star_1.transform.localScale;
        star_1.transform.localScale = new Vector2(0, 0);
        star_2.transform.localScale = new Vector2(0, 0);
        star_3.transform.localScale = new Vector2(0, 0);
        get_started();
    }

    public void get_started() {
        level_text.transform.DOMove(level_text_old_pos, anim_range+0.2f).SetEase(Ease.OutBack).OnComplete(() => stars_anim());


    }
    public void stars_anim() {
        Sequence seq;
        seq = DOTween.Sequence();
        Tween tween = star_1.transform.DOScale(stars_old_scale, anim_range).SetEase(Ease.OutBack);
        Tween tween2 = star_2.transform.DOScale(stars_old_scale, anim_range).SetEase(Ease.OutBack);
        Tween tween3 = star_3.transform.DOScale(stars_old_scale, anim_range).SetEase(Ease.OutBack);
        Tween tween4 = button.transform.DOMove(button_old_pos, anim_range + 0.2f).SetEase(Ease.OutBack);

        seq.Append(tween).Append(tween2).Append(tween3).Append(tween4);
    }
    public void skip_button() {
        level_text.transform.DOMove(level_text_new_pos, anim_range + 0.2f).SetEase(Ease.InBack);

        if (PlayerPrefs.GetInt("current_level") != 5) button.transform.DOMove(button_new_pos, anim_range + 0.2f).SetEase(Ease.InBack).OnComplete(() => Application.LoadLevel((PlayerPrefs.GetInt("current_level") + 1).ToString()));
        else button.transform.DOMove(button_new_pos, anim_range + 0.2f).SetEase(Ease.InBack).OnComplete(() => Application.LoadLevel("1"));
        star_1.transform.DOScale(new Vector2(0,0), anim_range).SetEase(Ease.InBack);
        star_2.transform.DOScale(new Vector2(0, 0), anim_range).SetEase(Ease.InBack);
        star_3.transform.DOScale(new Vector2(0, 0), anim_range).SetEase(Ease.InBack);
    }

}
