using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR
{
    public class ViewTypeMenu : MonoBehaviour
    {
        public int ViewTypeID;
        public VRController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("GameController"))
            {
                controller.ViewTypeNetworker.transform.localPosition = new Vector3(ViewTypeID, 0f, 0f);
                controller.removeMenu();
                controller.isChanging = false;
            }
        }
    }

}