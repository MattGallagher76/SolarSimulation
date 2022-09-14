 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject JoinedRoomPanel;
    public void ClickedHeadsetUser()
    {
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds()); //wait for few seconds to connect the server
    }
    public void ClickedOne()
    {
        PhotonNetwork.NickName = "1";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds()); //wait for few seconds to connect the server
    }
    public void ClickedTwo()
    {
        PhotonNetwork.NickName = "2";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedThree()
    {
        PhotonNetwork.NickName = "3";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedFour()
    {
        PhotonNetwork.NickName = "4";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedFive()
    {
        PhotonNetwork.NickName = "5";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedSix()
    {
        PhotonNetwork.NickName = "6";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedSeven()
    {
        PhotonNetwork.NickName = "7";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedEight()
    {
        PhotonNetwork.NickName = "8";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedNine()
    {
        PhotonNetwork.NickName = "9";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }
    public void ClickedTen()
    {
        PhotonNetwork.NickName = "10";
        StartCoroutine(EnableJoinedRoomPanelAfterFewSeconds());
    }



    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();

        if (LobbyManager.userType) //if it is true, ViewFinder User
        {
            //do nothing
        }
        else //if it is headset user
        {
            PhotonNetwork.NickName = "VR Headset Network Player";
        }

        Debug.Log("Try Connect to Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server");
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby(); //if the server is connected, automatically joined the lobby
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //do nothing
        }
        else
        {
            PhotonNetwork.LoadLevel(0); // go back to lobby
        }

    }
    public void InitializeRoom() //join or create room1
    {
        //Room option
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            IsVisible = true,
            IsOpen = true,
            PublishUserId = true
        };

        PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    IEnumerator EnableJoinedRoomPanelAfterFewSeconds()
    {
        yield return new WaitForSeconds(2.5f);
        JoinedRoomPanel.SetActive(true);
    }
}
