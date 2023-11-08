using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<GameObject> roomEnemies;
    private bool roomComplete;
    public List<GameObject> roomDoors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roomEnemies[0] == null & roomEnemies[1] == null & roomEnemies[2] == null & roomEnemies[3] == null
            & roomEnemies[4] == null & roomEnemies[5] == null & roomEnemies[6] == null & roomEnemies[7] == null)
        {
            roomComplete = true;

            for (int i = 0; i < roomDoors.Count; i++)
            {
                if (roomDoors[i].activeInHierarchy == false)
                {
                    roomDoors[i].SetActive(true);
                }
            }
        }
    }
}
