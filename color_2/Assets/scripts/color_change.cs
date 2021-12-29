using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color_change : MonoBehaviour
{
    GameObject hit_obj = null;
    Vector2 hit_obj_old_scale = Vector2.zero;
    public GameObject main;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.GetComponent<images_sctipt>() != null)
            {

                if (hit_obj != null)
                {
                    if (transform.GetComponent<check_script>().check(hit_obj, hit.transform.gameObject) == false)
                    {
                        make_start();
                        //object_color_change(hit.transform.gameObject);
                    }
                    else
                    {
                        transform.GetComponent<check_script>().change(hit_obj, hit.transform.gameObject);

                    }

                }
                else
                {
                    object_color_change(hit.transform.gameObject);
                }


            }
            else { if (hit_obj != null) { make_start(); hit_obj = null; } }//boþ bir ekrana týklamasý durumunda
        }
        if (Input.GetKeyUp("l"))
        {
            for (int i = 0; i < main.transform.childCount; i++)
            {
                main.transform.GetChild(i).GetComponent<images_sctipt>().color_anim_kill();
            }

        }
    }
    public void make_start()
    {
        for (int i = 0; i < main.transform.childCount; i++)
        {
            main.transform.GetChild(i).GetComponent<images_sctipt>().color_anim_kill();
            hit_obj.transform.DOScale(hit_obj_old_scale, 0.3f).SetEase(Ease.OutBack);

        }
        hit_obj = null;
    }
    public void object_color_change(GameObject hit)
    {
        hit_obj = hit.transform.gameObject;
        hit_obj_old_scale = hit.transform.localScale;
        //hit.transform.SetAsLastSibling();
        hit.transform.DOScale(new Vector2(hit.transform.localScale.x + 0.2f, hit.transform.localScale.y + 0.2f), 0.3f).SetEase(Ease.OutBack);
        Color hit_color = hit.transform.GetComponent<Image>().color;
        for (int i = 0; i < hit.transform.GetComponent<images_sctipt>().items.Count; i++)
        {
            hit.transform.GetComponent<images_sctipt>().items[i].GetComponent<images_sctipt>().color_anim(hit.transform.gameObject);
        }
    }
}
