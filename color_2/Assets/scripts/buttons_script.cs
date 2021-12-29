using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttons_script : MonoBehaviour
{
    public Color[] back_colors;
    public Color[] front_colors;
    public Color[] temp;
    public GameObject main;
    public GameObject back_button_obj;
    int change_object_number = 20;

    void Start()
    {
        back_colors = new Color[main.transform.childCount];
        front_colors = new Color[main.transform.childCount];
        temp = new Color[main.transform.childCount];
        for (int i = 0; i < front_colors.Length; i++)
        {

            front_colors[i] = main.transform.GetChild(i).GetComponent<Image>().color;
            temp[i] = main.transform.GetChild(i).GetComponent<Image>().color;
            back_colors[i] = new Color(0, 0, 0, 255);
        }
    }

    public void restart_button()
    {

        transform.GetComponent<start_script>().restart_anim();
    }
    public void back_button()
    {
        if (back_colors[0] != new Color(0, 0, 0, 255)) {
            for (int i = 0; i < back_colors.Length; i++)
            {
                main.transform.GetChild(i).GetComponent<Image>().color = back_colors[i];
                main.transform.GetChild(i).GetComponent<images_sctipt>().main_color = main.transform.GetChild(i).GetComponent<Image>().color;
            }
            Vector2 old_scale = main.transform.GetChild(change_object_number).transform.localScale;
            Sequence seq;
            seq = DOTween.Sequence();
            Tween tween = main.transform.GetChild(change_object_number).transform.DOScale(new Vector2(main.transform.GetChild(change_object_number).transform.localScale.x+0.2f, main.transform.GetChild(change_object_number).transform.localScale.y + 0.2f), 0.2f);
            Tween tween2 = main.transform.GetChild(change_object_number).transform.DOScale(old_scale, 0.2f);
            seq.Append(tween).Append(tween2);
            back_button_anim();

        }

    }
    public void back_button_anim() {
        Destroy(back_button_obj.transform.GetChild(0).gameObject);
        back_button_obj.transform.DOMove(new Vector2(back_button_obj.transform.position.x - 10, back_button_obj.transform.position.y), 0.7f).SetEase(Ease.InBack);
    }
    public void change_check()
    {
        bool is_same = true;
        for (int i = 0; i < front_colors.Length; i++)
        {
            front_colors[i] = main.transform.GetChild(i).GetComponent<Image>().color;

        }
        for (int i = 0; i < front_colors.Length; i++)
        {
            if (front_colors[i] != temp[i])
            {
                is_same = false;
                change_object_number = i;
                break;
            }


        }
        if (is_same == false)
        {
            for (int i = 0; i < front_colors.Length; i++)
            {
                back_colors[i] = temp[i];
                temp[i] = front_colors[i];


            }


        }

    }



}
