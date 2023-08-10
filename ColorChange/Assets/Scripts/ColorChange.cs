using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    private GameObject hitObject = null;
    private Vector2 hitObjectOldScale = Vector2.zero;
    public GameObject mainPanel;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseClick();
        }

        if (Input.GetKeyUp("l"))
        {
            KillColorAnimations();
        }
    }

    private void HandleMouseClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.transform != null)
        {
            ImagesScript hitImagesScript = hit.transform.GetComponent<ImagesScript>();

            if (hitImagesScript != null)
            {
                if (hitObject != null)
                {
                    CheckScript checkScript = transform.GetComponent<CheckScript>();

                    if (!checkScript.Check(hitObject, hit.transform.gameObject))
                    {
                        MakeStart();
                    }
                    else
                    {
                        checkScript.Change(hitObject, hit.transform.gameObject);
                    }
                }
                else
                {
                    ObjectColorChange(hit.transform.gameObject);
                }
            }
            else
            {
                if (hitObject != null)
                {
                    MakeStart();
                    hitObject = null;
                }
            }
        }
    }

    private void KillColorAnimations()
    {
        for (int i = 0; i < mainPanel.transform.childCount; i++)
        {
            mainPanel.transform.GetChild(i).GetComponent<ImagesScript>().ColorAnimKill();
        }
    }

    public void MakeStart()
    {
        for (int i = 0; i < mainPanel.transform.childCount; i++)
        {
            ImagesScript imagesScript = mainPanel.transform.GetChild(i).GetComponent<ImagesScript>();
            imagesScript.ColorAnimKill();
            hitObject.transform.DOScale(hitObjectOldScale, 0.3f).SetEase(Ease.OutBack);
        }

        hitObject = null;
    }

    public void ObjectColorChange(GameObject hit)
    {
        hitObject = hit.transform.gameObject;
        hitObjectOldScale = hit.transform.localScale;

        hit.transform.DOScale(hitObjectOldScale + new Vector2(0.2f, 0.2f), 0.3f).SetEase(Ease.OutBack);
        Color hitColor = hit.transform.GetComponent<Image>().color;

        for (int i = 0; i < hit.transform.GetComponent<ImagesScript>().items.Count; i++)
        {
            hit.transform.GetComponent<ImagesScript>().items[i].GetComponent<ImagesScript>().ColorAnim(hit.transform.gameObject);
        }
    }
}