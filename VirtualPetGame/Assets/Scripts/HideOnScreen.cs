using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnScreen : MonoBehaviour
{
    public bool isHidden;
    public float xOnScreen, yOnScreen;
    public float xHidden, yHidden;
    public float lerpSpeed;

    private RectTransform rt;

    private Vector2 target;

    void Start()
    {
        rt = this.GetComponent<RectTransform>();

        isHidden = false;
    }

    void Update()
    {
        if (isHidden)
        {
            target = new Vector2(xHidden, yHidden);
        }
        else
        {
            target = new Vector2(xOnScreen, yOnScreen);
        }


        rt.anchoredPosition = Vector3.Lerp(rt.anchoredPosition, target, lerpSpeed * Time.deltaTime);
    }

    public void Hide()
    {
        target = new Vector2(xHidden, yHidden);
    }

    public void Show()
    {
        target = new Vector2(xOnScreen, yOnScreen);
    }
}
