using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour
{
    private Chao myParent;
    private SpriteRenderer sp;
    private Animator emoteAnim;

    void Start()
    {
        myParent = this.transform.parent.gameObject.GetComponent<Chao>();
        sp = this.GetComponent<SpriteRenderer>();
        emoteAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageHeightOnJump();

        ManageAnimations();
    }

    void ManageAnimations()
    {
        if (myParent.isQuestion)
        {
            emoteAnim.SetInteger("EmoteAnimState", 8);
        }
        else if (myParent.isNo)
        {
            emoteAnim.SetInteger("EmoteAnimState", 7);
        }
        else if (myParent.isHappy)
        {
            emoteAnim.SetInteger("EmoteAnimState", 9);
        }
        else if (myParent.isSleeping)
        {
            emoteAnim.SetInteger("EmoteAnimState", 2);
        }
        else
        { 
            emoteAnim.SetInteger("EmoteAnimState", 0);
        }
    }

    void ManageHeightOnJump()
    {
        if (myParent.canMove)
        {
            sp.enabled = false;
        }
        else
        {
            sp.enabled = true;
        }
    }
}
