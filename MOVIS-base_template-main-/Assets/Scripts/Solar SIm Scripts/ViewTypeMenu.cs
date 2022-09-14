using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTypeMenu : MonoBehaviour
{
    public int ViewTypeID;
    public VRController controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("GameController"))
        {
            controller.changeScene(ViewTypeID);
        }
    }
}
