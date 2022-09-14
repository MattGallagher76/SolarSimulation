using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Tooltip("true is viewFinder, false is headset")]
    public static bool userType; 

    public void ClickedHeadset()
    {
        userType = false; //set as Headset user
    }
    public void ClickedViewFinder()
    {
        userType = true; //set as ViewFinder user
    }

    public void ClickedEnterRoom()
    {
        PhotonNetwork.LoadLevel(1); //load the Scene 1 (Room1)
    }
}
