using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Any script that needs to run directly with the VR system needs to operate in the Valve.VR namespace
 */
namespace Valve.VR
{
    /*
     * This class is responsible for allowing the VR user to select menu items and then moves the network object to the correct location.
     */
    public class SteamVRViewTypeMenu : MonoBehaviour
    {
        public int ViewTypeID; //Determins which viewtype this menu item is related to
        public SteamVRController controller; //The script that created this menu item

        /*
         * This script runs when a controller object touches this object. 
         * Once the method detects a collision, it moves the network object to tell the tablets and computer to change viewtypes.
         */
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