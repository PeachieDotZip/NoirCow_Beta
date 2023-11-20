/*****************************************************************************
// File Name :         EffectScript.cs
// Author :            Harrison Weber
// Creation Date :     September 25st, 2023
//
// Brief Description : General-purpose script to be used with all visual effects so that they may destroy themselves.
                       Unique code can also be run with other functions as animation events.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float effectTime = 1f;
    private GameManager gameManager;
    public int effectID;
    private AudioSource bashSFX;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bashSFX = GetComponent<AudioSource>();
    }
    public void BashSound()
    {
        switch (effectID)
        {
            case 0:
                bashSFX.pitch = Random.Range(0.95f, 1.15f);
                bashSFX.Play();
                break;
            case 1:
                bashSFX.Play();
                break;
            case 2:
                Debug.Log("boss pop lol");
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Used for slowdown effect while in animation.
    /// </summary>
    private void Update()
    {
        Time.timeScale = effectTime;
    }
    /// <summary>
    /// Destroys the effect after it plays it's animation. Called via animation event.
    /// </summary>
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
