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
    public GameManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
