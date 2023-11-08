/*****************************************************************************
// File Name :         UmbrellaBehaviour.cs
// Author :            Harrison Weber
// Creation Date :     September 21st, 2023
//
// Brief Description : Controls how the umbrella behaves. Also handles the interactions between the umbrella and its environment.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UmbrellaBehaviour : MonoBehaviour
{
    public CowController cow;
    private Animator anim;
    public bool isPoking;
    public bool isOpen;
    public bool isBashing;
    public AudioSource openSFX;
    public AudioSource closeSFX;
    public AudioSource bashSwingSFX;

    private void Awake()
    {
        cow = FindObjectOfType<CowController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(TakeUmbrella());
    }
    private void Update()
    {

        if (cow.cowActions.Player.UmbrellaPoke.triggered)
        {
            UmbrellaPoke();
        }
        if (cow.cowActions.Player.UmbrellaOpen.triggered)
        {
            UmbrellaOpen();
        }

        if (isOpen)
        {
            cow.speed = 5f;
        }
        else
        {
            cow.speed = 7.2f;
        }

        //vvv Umbrella Variable Control -----
        isOpen = cow.cowActions.Player.UmbrellaOpen.IsPressed();
        //^^^ Umbrella Variable Control -----



        //vvv Animation Variables -----
        //anim.SetBool("isPoking", isPoking); // unused
        anim.SetBool("isOpen", isOpen);
        //anim.SetBool("isBashing", isBashing); //unused
        //^^^ Animation Variables -----
    }
    private void UmbrellaPoke()
    {
        if (isOpen == false)
        {
            anim.SetTrigger("Poke");
        }
        else
        {
            anim.SetTrigger("Bash");
        }
    }
    private void UmbrellaOpen()
    {

    }
    public void UmbrellaOpenSFX()
    {
        openSFX.Play();
    }
    public void UmbrellaCloseSFX()
    {
        closeSFX.Play();
    }


    private IEnumerator TakeUmbrella()
    {
        closeSFX.Play();
        yield return new WaitForSeconds(0.21f);
        gameObject.SetActive(false);
    }
    // The following functions are used within animation events to control certain variables and interactions.

    public void PokeVoid1()
    {
        Debug.Log("Poke!");
        isPoking = true;
    }
    public void PokeVoid2()
    {
        isPoking = false;
    }
    public void BashVoid1()
    {
        Debug.Log("Bash!");
        isBashing = true;
        bashSwingSFX.Play();
    }
    public void BashVoid2()
    {
        Debug.Log("Bash End");
        isBashing = false;
    }
}
