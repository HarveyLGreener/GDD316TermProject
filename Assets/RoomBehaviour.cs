using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls;
    // Start is called before the first frame update
    public void UpdateRoom(bool[] status)
    {
        for (int index = 0; index < status.Length; index++)
        {
            if (status[index])
            {
                walls[index].SetActive(false);
            }
        }
    }
}
