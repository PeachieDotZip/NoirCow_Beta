/*****************************************************************************
// File Name :         DoorScript.cs
// Author :            Harrison Weber
// Creation Date :     October 10th, 2023
//
// Brief Description : Controls how the player maneuvers throughout the level.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator UIanim;
    public GameObject newRoomTelePoint; // Point in space that the player will get teleported to when they enter this door.
    private CanvasScript canvasScript;
    public float nextRoomCameraSize;
    public Transform nextRoomCameraPosition;
    public int doorID;
    // --- DOOR IDs FOR ANIMATIONS: ---
    // 0 = up
    // 1 = down
    // 2 = left
    // 3 = right


    void Start()
    {
        UIanim = GameObject.Find("Canvas").GetComponent<Animator>();
        canvasScript = FindObjectOfType<CanvasScript>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterDoor();
        }
    }

    /// <summary>
    /// Checks the ID of this door before telling the canvas to play the correct animation.
    /// The UI animation will teleport the player to the given room.
    /// </summary>
    private void EnterDoor()
    {
        switch (doorID)
        {
            default:
                Debug.Log("Door ID is out of acceptable range!");
                break;
            case 0:
                DefineNextRoom();
                UIanim.SetTrigger("up");
                Debug.Log("Moving Up");
                break;
            case 1:
                DefineNextRoom();
                UIanim.SetTrigger("down");
                Debug.Log("Moving Down");
                break;
            case 2:
                DefineNextRoom();
                UIanim.SetTrigger("left");
                Debug.Log("Moving Left");
                break;
            case 3:
                DefineNextRoom();
                UIanim.SetTrigger("right");
                Debug.Log("Moving Right");
                break;
        }
    }

    /// <summary>
    /// Tells CanvasScript what gameObject to teleport the player to.
    /// The gameObject to teleport to is assigned in inspector as "newRoomTelePoint".
    /// </summary>
    private void DefineNextRoom()
    {
        canvasScript.newRoom = newRoomTelePoint;
        canvasScript.cameraSize = nextRoomCameraSize;
        canvasScript.cameraPosition = nextRoomCameraPosition;
    }
}
