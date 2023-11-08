/*****************************************************************************
// File Name :         ObjectBehaviour.cs
// Author :            Harrison Weber
// Creation Date :     September 21st, 2023
//
// Brief Description : Mainly used for debugging as of now. Records when the umbrella is poking, then responds to being poked.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    private UmbrellaBehaviour umbrella;
    private Animation anim;
    private ParticleSystem dust;
    public bool doesGiveHiddenObject;
    public GameObject hiddenObject;

    // Start is called before the first frame update
    void Start()
    {
        umbrella = FindObjectOfType<UmbrellaBehaviour>();
        anim = GetComponent<Animation>();
        dust = GetComponentInChildren<ParticleSystem>();

        if (doesGiveHiddenObject == false)
        {
            hiddenObject = null;
        }
        else
        {
            hiddenObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Umbrella"))
        {
            Debug.Log("object touched by umbrella");
            if (umbrella.isPoking == true)
            {
                Debug.Log("object poked");
                anim.Play();
                dust.Play();
                if (doesGiveHiddenObject == true)
                {
                    hiddenObject.SetActive(true);
                }
            }
        }
    }
}
