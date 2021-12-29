using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start_script : MonoBehaviour
{
    public GameObject main;
    Vector2[] old_pos;
    Vector2[] new_pos;
    float range_images = 10f;
    float start_anim_range = 0.6f;
    public GameObject buttons_top;
    Vector2 buttons_top_old_pos;
    public GameObject paint;
    Vector2 paint_old_pos;
    void Start()
    {
        if (!PlayerPrefs.HasKey("current_level"))
        {
            PlayerPrefs.SetInt("current_level", 1);
        }


        old_pos = new Vector2[main.transform.childCount];
        new_pos = new Vector2[main.transform.childCount];

        for (int i = 0; i < old_pos.Length; i++)
        {
            old_pos[i] = main.transform.GetChild(i).transform.position;
        }
        for (int i = 0; i < main.transform.childCount; i++)
        {
            int rand = Random.Range(1, 5);
            if (rand == 1) main.transform.GetChild(i).transform.position = new Vector2(main.transform.GetChild(i).transform.position.x + range_images, main.transform.GetChild(i).transform.position.y);
            else if (rand == 2) main.transform.GetChild(i).transform.position = new Vector2(main.transform.GetChild(i).transform.position.x - range_images, main.transform.GetChild(i).transform.position.y);
            else if (rand == 3) main.transform.GetChild(i).transform.position = new Vector2(main.transform.GetChild(i).transform.position.x, main.transform.GetChild(i).transform.position.y + range_images);
            else if (rand == 4) main.transform.GetChild(i).transform.position = new Vector2(main.transform.GetChild(i).transform.position.x, main.transform.GetChild(i).transform.position.y - range_images);

        }
        for (int i = 0; i < main.transform.childCount; i++)
        {
            new_pos[i] = main.transform.GetChild(i).transform.position;
        }
        buttons_top_old_pos = buttons_top.transform.position;
        buttons_top.transform.position = new Vector2(buttons_top.transform.position.x - range_images, buttons_top.transform.position.y );

        paint_old_pos = paint.transform.position;
        paint.transform.position = new Vector2(paint.transform.position.x + range_images, paint.transform.position.y );
        StartCoroutine(wait2());
    }
    IEnumerator wait2() {
        yield return new WaitForSeconds(0.5f);
        start_anim();

    }

    void Update()
    {
        if (Input.GetKeyUp("t"))
        {

            start_anim();
        }
        if (Input.GetKeyUp("y"))
        {

            restart_anim();
        }
    }
    public void start_anim()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {

            Color main_color = new Color(main.transform.GetChild(i).GetComponent<Image>().color.r, main.transform.GetChild(i).GetComponent<Image>().color.g, main.transform.GetChild(i).GetComponent<Image>().color.b, main.transform.GetChild(i).GetComponent<Image>().color.a);
            Vector2 old_scale = main.transform.GetChild(i).transform.localScale;


            main.transform.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            main.transform.GetChild(i).localScale = new Vector2(0, 0);


            main.transform.GetChild(i).transform.DOMove(old_pos[i], start_anim_range).SetEase(Ease.OutBack).OnComplete(() =>open_triggers());
            main.transform.GetChild(i).GetComponent<Image>().DOColor(main_color, start_anim_range);
            main.transform.GetChild(i).transform.DOScale(old_scale, start_anim_range);

        }
        StartCoroutine(wait());


    }
    public void restart_anim()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).transform.DOMove(new_pos[i], start_anim_range).SetEase(Ease.InBack).OnComplete(() => Application.LoadLevel(SceneManager.GetActiveScene().name));
            main.transform.GetChild(i).GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), start_anim_range);
            main.transform.GetChild(i).transform.DOScale(new Vector2(0, 0), start_anim_range);

        }
        buttons_top.transform.DOMove(new Vector2(buttons_top.transform.position.x - range_images, buttons_top.transform.position.y), start_anim_range).SetEase(Ease.InBack);
        paint.transform.DOMove(new Vector2(paint.transform.position.x + range_images, paint.transform.position.y), start_anim_range).SetEase(Ease.InBack);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(start_anim_range);
        buttons_top.transform.DOMove(buttons_top_old_pos, start_anim_range).SetEase(Ease.OutBack);
        paint.transform.DOMove(paint_old_pos, start_anim_range).SetEase(Ease.OutBack);
    }
    void open_triggers() {
        for (int i = 0; i < main.transform.childCount; i++) {
            main.transform.GetChild(i).GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
    public void finis_anim()
    {//SceneManager.GetActiveScene().name
        int temp = int.Parse(SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("current_level", temp);
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).transform.DOMove(new_pos[i], start_anim_range).SetEase(Ease.InBack).OnComplete(() => Application.LoadLevel("level_scane"));
            main.transform.GetChild(i).GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), start_anim_range);
            main.transform.GetChild(i).transform.DOScale(new Vector2(0, 0), start_anim_range);

        }
        buttons_top.transform.DOMove(new Vector2(buttons_top.transform.position.x - range_images, buttons_top.transform.position.y ), start_anim_range).SetEase(Ease.InBack);
        paint.transform.DOMove(new Vector2(paint.transform.position.x + range_images, paint.transform.position.y), start_anim_range).SetEase(Ease.InBack);
    }
}
