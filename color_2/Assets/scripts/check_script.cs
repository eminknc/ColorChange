using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check_script : MonoBehaviour
{
    public GameObject main;
    public GameObject tablo;

    void Start()
    {

    }

    void Update()
    {

    }
    public bool check(GameObject hit_1, GameObject hit_2)
    {
        bool checkk = false;
        for (int i = 0; i < hit_1.transform.GetComponent<images_sctipt>().items.Count; i++)
        {
            if (hit_1.transform.GetComponent<images_sctipt>().items[i] == hit_2.gameObject) checkk = true;

        }
        if (checkk)
        {
            check_finish();
            hit_2.transform.GetComponent<images_sctipt>().main_color = hit_1.transform.GetComponent<Image>().color;
            return true;
        }
        else
        {
            hit_1.GetComponent<Outline>().effectColor = new Color(255, 0, 0, 255);
            hit_2.GetComponent<Outline>().effectColor = new Color(255, 0, 0, 255);
            hit_1.GetComponent<Outline>().DOColor(new Color(0, 0, 0, 255), 0.5f);
            hit_2.GetComponent<Outline>().DOColor(new Color(0, 0, 0, 255), 0.5f);
            return false;
        }
    }
    public void change(GameObject hit_1, GameObject hit_2)
    {
        hit_2.transform.GetComponent<Image>().DOColor(hit_1.transform.GetComponent<Image>().color, 0.5f).OnComplete(() => transform.GetComponent<buttons_script>().change_check()); ;
        hit_2.transform.localScale = new Vector2(hit_2.transform.localScale.x + 0.2f, hit_2.transform.localScale.y + 0.2f);
        hit_2.transform.DOScale(new Vector2(hit_2.transform.localScale.x - 0.2f, hit_2.transform.localScale.y - 0.2f), 0.3f);
        
        transform.GetComponent<color_change>().make_start();

    }
    public void check_finish()
    {

        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.4f);
        bool check_finishx = true;
        for (int i = 0; i < main.transform.childCount; i++)
        {
            if (main.transform.GetChild(i).GetComponent<Image>().color != tablo.transform.GetChild(i).GetComponent<Image>().color)
            {
                check_finishx = false;
            }

        }
        if (check_finishx)
        {

            transform.GetComponent<start_script>().finis_anim();
        }
    }
}
