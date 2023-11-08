/*****************************************************************************
// File Name :         InspectableObject.cs
// Author :            Harrison Weber
// Creation Date :     September 27th, 2023
//
// Brief Description : Controls how inspectable objects behave, what sprites they use, and how they are animated.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    public CowController cow;
    public Animator uiAnim;
    public bool isInspecting;
    public int objectSide;
    private SpriteRenderer spriteR;
    public Sprite[] sprites;
    public float objRadius = 3f;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInspecting)
        {
            cow.speed = 0f;
            uiAnim.SetInteger("ObjectSide", objectSide);

            if (cow.cowActions.Player.Flip.triggered)
            {
                uiAnim.SetTrigger("Flip");
            }
            if (cow.cowActions.Player.Inspect.triggered)
            {
                PutDownObject();
            }
            if (uiAnim.GetCurrentAnimatorStateInfo(0).IsName("UI_inspect_debug1"))
            {
                objectSide = 0;
            }
            if (uiAnim.GetCurrentAnimatorStateInfo(0).IsName("UI_inspect_debug2"))
            {
                objectSide = 1;
            }
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, cow.gameObject.transform.position) < objRadius)
            {
                if (cow.cowActions.Player.Inspect.triggered)
                {
                    uiAnim.SetTrigger("Inspect");
                    uiAnim.SetBool("canFlip", true);
                    isInspecting = true;
                    objectSide = 0;
                }
            }
        }
    }

    public IEnumerator FlipSides(SpriteRenderer renderer)
    {
        switch (objectSide)
        {
            case 0:
                objectSide = 1;
                renderer.sprite = sprites[objectSide];
                break;
            case 1:
                objectSide = 0;
                renderer.sprite = sprites[objectSide];
                break;
            default:
                Debug.Log("lol");
                break;

        }
        uiAnim.ResetTrigger("Flip");
        uiAnim.SetBool("canFlip", false);
        yield return new WaitForSeconds(0.6f);
        uiAnim.SetBool("canFlip", true);
    }

    public void PutDownObject()
    {
        Debug.Log(gameObject.name + " was put down.");
        uiAnim.SetTrigger("ReturnToIdle");
        isInspecting = false;
        cow.speed = 7.2f;
        objectSide = 0;
    }
    //Draw Radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, objRadius);
    }
}
