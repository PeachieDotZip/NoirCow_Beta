/*****************************************************************************
// File Name :         KeyBehaviour.cs
// Author :            Harrison Weber
// Creation Date :     October 29th, 2023
//
// Brief Description : Handles how the keys are counted and the physical key collection.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private Animator anim;
    private GameManager gameManager;
    public GameObject collectEffect;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Collected");
        }
    }

    /// <summary>
    /// Called during key collecting animation. Adds 1 to the amount of keys the player has collected.
    /// </summary>
    public void CollectKey()
    {
        Instantiate(collectEffect, gameObject.transform.position, Quaternion.identity);
        gameManager.keyAmount += 1;
    }

    /// <summary>
    /// Destroys gameobject at the end of its animation.
    /// </summary>
    public void DestroySelf()
    {
        gameManager.canvasAnim.SetTrigger("down");
        gameManager.CheckKeyAmount();
        Destroy(gameObject);
    }
}
