using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class CameraSetup : MonoBehaviour
{
    private GameObject Tracker1, Tracker2, Tracker3, Tracker4, Tracker5, Tracker6, Tracker7, Tracker8, Tracker9;
    private Canvas canvas;
    private TextMeshProUGUI deviceNumberText;

    private PhotonView photonView;
    [SerializeField]
    private GameObject[] viewFinderCameras;

    public Quaternion cameraOffset;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        deviceNumberText = GameObject.Find("Canvas/DeviceNumberTMP").GetComponent<TextMeshProUGUI>();

        photonView = GetComponent<PhotonView>();
        StartCoroutine(FindTrackerAfterFewSeconds()); //give few seconds for the systems to settle
        this.gameObject.name = photonView.Owner.NickName;

        if (photonView.IsMine) //revmoe the tag so that myself is not disabled in the update funciton
        {
            this.gameObject.tag = "Untagged";
        }

        deviceNumberText.text = "#" + photonView.Owner.NickName; //print the device number on the screen
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.Owner.NickName == "1") { MapTrackerPosition(Tracker1); }
        else if (photonView.Owner.NickName == "2"){ MapTrackerPosition(Tracker2); }
        else if (photonView.Owner.NickName == "3") { MapTrackerPosition(Tracker3); }
        else if (photonView.Owner.NickName == "4") { MapTrackerPosition(Tracker4); }
        else if (photonView.Owner.NickName == "5") { MapTrackerPosition(Tracker5); }
        else if (photonView.Owner.NickName == "6") { MapTrackerPosition(Tracker6); }
        else if (photonView.Owner.NickName == "7") { MapTrackerPosition(Tracker7); }
        else if (photonView.Owner.NickName == "8") { MapTrackerPosition(Tracker8); }
        else if (photonView.Owner.NickName == "9") { MapTrackerPosition(Tracker9); }


        //disable the camera that is not my view
        viewFinderCameras = GameObject.FindGameObjectsWithTag("ViewFinderCamera");
        for (int i = 0; i < viewFinderCameras.Length; i++)
        {
            viewFinderCameras[i].SetActive(false);
        }

    }

    void MapTrackerPosition(GameObject tracker)
    {
        try
        {
            Vector3 newPosition = tracker.transform.position;
            Quaternion newRotation = tracker.transform.rotation;

            newPosition.y += .1f;
            newRotation *= cameraOffset;

            transform.position = newPosition;
            transform.rotation = newRotation;
            //transform.position = tracker.transform.position;
            //transform.rotation = tracker.transform.rotation;
        }
        catch
        {
            Debug.Log("Loading a player...");
        }
    }

    IEnumerator FindTrackerAfterFewSeconds()
    {
        yield return new WaitForSeconds(3f);
        Tracker1 = GameObject.Find("Tracker1(Clone)");
        Tracker2 = GameObject.Find("Tracker2(Clone)");
        Tracker3 = GameObject.Find("Tracker3(Clone)");
        Tracker4 = GameObject.Find("Tracker4(Clone)");
        Tracker5 = GameObject.Find("Tracker5(Clone)");
        Tracker6 = GameObject.Find("Tracker6(Clone)");
        Tracker7 = GameObject.Find("Tracker7(Clone)");
        Tracker8 = GameObject.Find("Tracker8(Clone)");
        Tracker9 = GameObject.Find("Tracker9(Clone)");
    }

}

