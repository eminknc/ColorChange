using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class images_sctipt : MonoBehaviour
{
    public List<GameObject> items;
    Sequence seq;
    public Color main_color;
    private void Start()
    {
        //itemBoxCollider2D.size = image.rectTransform.rect.size;
        transform.GetComponent<BoxCollider2D>().size =new Vector2(transform.GetComponent<RectTransform>().rect.size.x+20, transform.GetComponent<RectTransform>().rect.size.y+20) ;
        main_color = transform.GetComponent<Image>().color;
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (!items.Contains(other.gameObject))
        {
            if (other.transform.GetComponent<images_sctipt>() != null)
            {
                items.Add(other.gameObject);
            }

        }
    }
    public void color_anim(GameObject hit_obj)
    {
        
        seq = DOTween.Sequence();
        seq.Append(transform.GetComponent<Image>().DOColor(hit_obj.GetComponent<Image>().color, 0.5f))
                       .Append(transform.GetComponent<Image>().DOColor(main_color, 0.5f))
                       .Append(transform.GetComponent<Image>().DOColor(main_color, 2f))
                       .SetLoops(10);
    

    }
    public void color_anim_kill()
    {
        transform.GetComponent<Image>().color=main_color;
        seq.Kill();

    }

}
