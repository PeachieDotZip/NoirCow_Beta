using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIcon : MonoBehaviour
{
    private Animator uiAnim;
    public InspectableObject inspection;
    private SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    {
        uiAnim = GetComponent<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipSides()
    {
        StartCoroutine(inspection.FlipSides(spriteR));
    }
}
