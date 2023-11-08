/*****************************************************************************
// File Name :         CowController.cs
// Author :            Harrison Weber
// Creation Date :     September 21st, 2023
//
// Brief Description : Controls all variables and interactions the player can have.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CowController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    public CowInput cowActions;
    private Animator umbrellaAnim;
    private GameManager gameManager;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject mainMenu;
    public GameObject howToPlay;
    private bool isPaused;


    /// <summary>
    /// Assigns cowActions to measure input.
    /// Assigns rigidbody and animator of umbrella.
    /// </summary>
    private void Awake()
    {
        cowActions = new CowInput();

        rb = GetComponent<Rigidbody2D>();
        umbrellaAnim = GetComponentInChildren<Animator>();

        gameManager = FindObjectOfType<GameManager>();

    }

    /// <summary>
    /// Controls how fast the player moves.
    /// </summary>
    private void FixedUpdate()
    {
        rb.velocity = movementInput * speed;
    }


    /// <summary>
    /// Debug prototype code, remove whenever
    /// </summary>
    private void Update()
    {

        if (cowActions.Player.Pause.triggered && isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;

            restartButton.SetActive(true);
            exitButton.SetActive(true);
            mainMenu.SetActive(true);
            howToPlay.SetActive(true);
        }
        else if (cowActions.Player.Pause.triggered && isPaused == true)
        {
            Time.timeScale = 1f;
            isPaused = false;

            restartButton.SetActive(false);
            exitButton.SetActive(false);
            mainMenu.SetActive(false);
            howToPlay.SetActive(false);
        }
    }


    /// <summary>
    /// Measures input and move player
    /// </summary>
    /// <param name="inputValue"></param>
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }


    private void OnEnable()
    {
        cowActions.Enable();
    }
    private void OnDisable()
    {
        cowActions.Disable();
    }
}