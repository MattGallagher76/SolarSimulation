using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameRoomManager : MonoBehaviour
{
    public GameObject headsetCamera;
    public Canvas canvas;
    void Start()
    {
        if(LobbyManager.userType == true) // if it is ViewFinder user
        {
            headsetCamera.SetActive(false);
            
        }
        else //if it is Headset user
        {
            headsetCamera.SetActive(true);
            canvas.enabled = false; //disable canvas (device nubmer and center dot) 
        }


    }

}
