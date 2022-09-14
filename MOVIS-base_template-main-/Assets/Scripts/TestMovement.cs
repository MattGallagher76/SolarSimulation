using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestMovement : MonoBehaviour
{
    private GameObject networkCube, networkSphere, networkCylinder;

    void Update()
    {
        

        if (Input.GetKeyDown("1"))
        {
            networkCube = PhotonNetwork.Instantiate("NetworkCube", new Vector3(0, 1, 3), Quaternion.identity);
        }
        if (Input.GetKeyDown("2"))
        {
            
        }

        if (Input.GetKey("w"))
        {
            networkCube.transform.Translate(Vector3.up * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            networkCube.transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

}
