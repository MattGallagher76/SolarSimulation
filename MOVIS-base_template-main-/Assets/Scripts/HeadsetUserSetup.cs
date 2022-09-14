using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// This is the source code we need to connect so that it can move according to the VIVE tracker.
public class HeadsetUserSetup : MonoBehaviour
{   
    private PhotonView photonView;
    public GameObject networkPlayer, networkPlayerHead, networkPlayerLeftHand, networkPlayerRightHand;
    private GameObject myPlayerHead, myPlayerLeftHand, myPlayerRightHand;
    
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (!LobbyManager.userType) // if it is VR Headset User,
        {
            myPlayerHead = GameObject.Find("VR Controller/[CameraRig]/Camera");
            myPlayerLeftHand = GameObject.Find("VR Controller/[CameraRig]/Controller (left)");
            myPlayerRightHand = GameObject.Find("VR Controller/[CameraRig]/Controller (right)");
        }
        else // if it is a viewFinder user, disabled this setup.
        {
            this.GetComponent<HeadsetUserSetup>().enabled = false;
        }

        this.gameObject.name = photonView.Owner.NickName;

    }
   
    // Update is called once per frame
    void Update()
    {        
        if (photonView.IsMine)
        {
            //networkPlayerHead.gameObject.SetActive(false);
            //networkPlayerLeftHand.gameObject.SetActive(false);
            //networkPlayerRightHand.gameObject.SetActive(false);

            MapTransform(networkPlayerHead.transform, myPlayerHead.transform);
            MapTransform(networkPlayerLeftHand.transform, myPlayerLeftHand.transform);
            MapTransform(networkPlayerRightHand.transform, myPlayerRightHand.transform);

        }
    }

    void MapTransform(Transform networkPlayer, Transform myTransform)
    {
        networkPlayer.position = myTransform.position;
        networkPlayer.rotation = myTransform.rotation;
    }
         
}
