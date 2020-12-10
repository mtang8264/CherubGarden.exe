using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    private Chao myParent;

    private SpriteRenderer sp;
    private Animator eyesAnim;

    public float blinkTime;
    private float blinkInterval;

    private int ParentAnimState;

    void Start()
    {
        myParent = this.transform.parent.gameObject.GetComponent<Chao>();
        sp = this.GetComponent<SpriteRenderer>();
        eyesAnim = this.GetComponent<Animator>();

        blinkInterval = blinkTime;
    }

    void Update()
    {
        ParentAnimState = myParent.anim.GetInteger("AnimState");

        ManageEyesHeightBlink();

        if (myParent.canMove)
        {
            eyesAnim.SetInteger("EyesAnimState", 1);
        }
        else if (myParent.isEating)
        {
            eyesAnim.SetInteger("EyesAnimState", 3);
        }
        else if (myParent.isWatering)
        {
            eyesAnim.SetInteger("EyesAnimState", 4);
        }
        else if (myParent.isEviling)
        {
            eyesAnim.SetInteger("EyesAnimState", 5);
        }
        else if (myParent.isFlower)
        {
            eyesAnim.SetInteger("EyesAnimState", 6);
        }
        else
        {
            eyesAnim.SetInteger("EyesAnimState", 0);
        }

        if (myParent.isSleeping)
        {
            eyesAnim.SetInteger("EyesAnimState", 2);
        }
        else
        {
            if (eyesAnim.GetInteger("EyesAnimState") == 2)
            {
                eyesAnim.SetInteger("EyesAnimState", 0);
            }
        }


    }

    private void ManageEyesHeightBlink()
    {
        /*blinkInterval -= Time.deltaTime;
        if (blinkInterval <= 0f)
        {
            if (ParentAnimState == 0)
            {
                StartCoroutine(Blink());
            }
            blinkInterval = blinkTime;
        }*/
    }

    IEnumerator Blink()
    {
        eyesAnim.SetBool("EyesBlink", true);
        yield return new WaitForSeconds(0.1f);
        eyesAnim.SetBool("EyesBlink", false);
    }
}
