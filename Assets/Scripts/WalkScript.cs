/*****************************************************************************
// File Name :         WalkScript.cs
// Author :            Harrison Weber
// Creation Date :     November 13th, 2023
//
// Brief Description : Simply controls how the sound effects for walking are played.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public AudioSource walkSFX;

    public void PlayWalkSFX()
    {
        walkSFX.pitch = Random.Range(0.9f, 1.1f);
        walkSFX.Play();
    }
}
