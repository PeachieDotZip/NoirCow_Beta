/*****************************************************************************
// File Name :         DebugProjectileBehavior.cs
// Author :            Lorien Nergard
// Creation Date :     September 21st, 2023
//
// Brief Description : A coroutine that fires a debug bullet.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugProjectileBehavior : MonoBehaviour
{
    public Transform shootPos;
    public GameObject bulletPrefab;
    public float shootTimer;

    private bool isShooting;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        isShooting = false;
    }

    /// <summary>
    ///  Update is called once per frame
    /// </summary>
    void Update()
    {
        if (isShooting == false)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            StopCoroutine(Shoot());
        }
    }

    /// <summary>
    /// The courotine that instantiates the prefab
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        isShooting = true;

        Instantiate(bulletPrefab, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
    }
}

