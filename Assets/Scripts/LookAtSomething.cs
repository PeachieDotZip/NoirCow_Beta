/*****************************************************************************
// File Name :         LookAttSomething.cs
// Author :            Harrison Weber
// Creation Date :     October 15th, 2023
//
// Brief Description : Rotates a gameObject towards another.
//                     IMPORTANT: Assumes the object's "front" is facing up.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSomething : MonoBehaviour
{
    public GameObject something;
    public bool isEnemy;

    /// <summary>
    /// If the object with this script is marked as an enemy, then the "something" object will immediately be set to player.
    /// </summary>
    private void Start()
    {
        if (isEnemy == true)
        {
            something = GameObject.Find("CowNoir");
        }
    }
    /// <summary>
    /// Update the rotation of the object to face the position of the given object.
    /// </summary>
    private void Update()
    {
        Vector3 offset = something.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );
    }
}