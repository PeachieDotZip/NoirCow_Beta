using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedUmbrellaScript : MonoBehaviour
{
    public GameObject umbrella;
    public GameObject tutorials;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            umbrella.SetActive(true);
            tutorials.SetActive(true);
            Destroy(gameObject);
        }
    }
}
