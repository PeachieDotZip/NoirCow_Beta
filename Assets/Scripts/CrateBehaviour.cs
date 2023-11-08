/*****************************************************************************
// File Name :         CrateBehaviour.cs
// Author :            Harrison Weber
// Creation Date :     October 16th, 2023
//
// Brief Description : Controls the interactions between the crates and the rest of the game world.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    private UmbrellaBehaviour umbrella;
    private Collider crateCol;
    public GameObject breakEffect;
    private bool containsObject;
    private int objectRandomInt;
    public GameObject containedObject;

    // Start is called before the first frame update
    void Start()
    {
        umbrella = FindObjectOfType<UmbrellaBehaviour>();
        crateCol = GetComponent<Collider>();

        // Whether or not this crate contains an object depends on if their is an object assigned to it in the inspector.
        if (containedObject == null)
        {
            containsObject = false;
        }
        else
        {
            containsObject = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Charging") || collision.gameObject.CompareTag("Enemy"))
        {
            BreakSelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Umbrella"))
        {
            Debug.Log("crate touched umbrella");
            if (umbrella.isBashing == true)
            {
                BreakSelf();
            }
        }
        if (collision.CompareTag("Bullet") || collision.CompareTag("Bullet_Ricochet") || collision.CompareTag("Bullet_Bash"))
        {
            Debug.Log("crate touched bullet");
            BreakSelf();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Umbrella"))
        {
            Debug.Log("crate touched umbrella");
            if (umbrella.isBashing == true)
            {
                BreakSelf();
            }
        }
    }

    public void BreakSelf()
    {
        if (containsObject)
        {
            objectRandomInt = Random.Range(0, 100);
            if (objectRandomInt <= 5)
            {
                Instantiate(containedObject, gameObject.transform.position, Quaternion.identity);
            }
        }
        Instantiate(breakEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
