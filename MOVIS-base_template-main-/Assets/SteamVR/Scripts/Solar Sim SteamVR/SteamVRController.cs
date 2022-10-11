using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Any script that needs to run directly with the VR system needs to operate in the Valve.VR namespace
 */
namespace Valve.VR
{
    /*
     * This object is attached to the parent object of the controllers. It is responsible for taking any and all inputs from the controllers and
     * doing the correct task depending on the input
     */
    public class SteamVRController : MonoBehaviour 
    {
        public float sensitivity; //How sensitive the trigger is for moving - DEVELOPER USE ONLY
        public float maxSpeed; //How fast the movement is - DEVELOPER USE ONLY

        public SteamVR_Action_Boolean LeftChangeViewType = null; //The action to change viewtypes on the left controller
        public SteamVR_Action_Boolean RightChangeViewType = null; //The action to change viewtypes on the right controller
        public SteamVR_Action_Single RightTrigger = null; //The action related to pulling the trigger. Currently used for movement - DEVELOPER USE ONLY
        public SteamVR_Action_Single LeftTrigger = null; //The action related to pulling the trigger. Currently used for movement - DEVELOPER USE ONLY

        public GameObject LeftHandPos; //The object with the current location of the left controller
        public GameObject RightHandPos; //The object with the current location of the right controller

        public GameObject LeftHandCol; //The object with the collider of the left controller
        public GameObject RightHandCol; //The object with the collider of the right controller

        public GameObject ViewTypeMenuPrefab; //The prefab for the menu object
        public GameObject ViewTypeNetworker; //The network object used for changing scenes

        private Vector3 speed = Vector3.zero;

        public bool isChanging = false;  //Boolean to represent if the user is trying to change viewtypes.
        private int ViewTypeCount = 3;//If adding a viewtype, change this value AND the one in PlanetData
        private GameObject[] menuItems; //An array of the menu objects created while in use.

        //Start method
        private void Start()
        {
            menuItems = new GameObject[ViewTypeCount];
        }

        //Upate method
        private void Update()
        {
            CalcMovement();//Only for testing, remove for demo.
            CheckMenu();
        }

        /*
         * When called in the update method, it checks if the controller viewtype buttons are down.
         * If that is the case, it creates the menu items around the corresponding controller.
         */
        private void CheckMenu()
        {
            if(LeftChangeViewType.state && !isChanging)
            {
                isChanging = true;
                for(int i = 0; i < ViewTypeCount; i ++)
                {
                    GameObject view = Instantiate(ViewTypeMenuPrefab, LeftHandPos.transform.position + LeftHandPos.transform.rotation * (new Vector3((i * 0.2f) - (ViewTypeCount * 0.1f / 2), 0.1f, 0)), transform.rotation);
                    view.GetComponent<SteamVRViewTypeMenu>().ViewTypeID = i + 1;
                    view.GetComponent<SteamVRViewTypeMenu>().controller = this;
                    menuItems[i] = view;
                }
            }
            else if (RightChangeViewType.state && !isChanging)
            {
                isChanging = true;
                for (int i = 0; i < ViewTypeCount; i++)
                {
                    GameObject view = Instantiate(ViewTypeMenuPrefab, RightHandPos.transform.position + RightHandPos.transform.rotation * (new Vector3((i * 0.2f) - (ViewTypeCount * 0.1f / 2), 0.1f, 0)), transform.rotation);
                    view.GetComponent<SteamVRViewTypeMenu>().ViewTypeID = i + 1;
                    view.GetComponent<SteamVRViewTypeMenu>().controller = this;
                    menuItems[i] = view;
                }
            }
        }

        /*
         * When called, it removes all the menu items that have been created.
         */
        public void removeMenu()
        {
            foreach(GameObject menu in menuItems)
            {
                Destroy(menu);
            }
        }

        /*
         * Moves the VR user when either of the triggers have been pulled.
         * Note that the user moves in the direction that the controller is pointing. 
         * DEVELOPER USE ONLY
         */ 
        private void CalcMovement()
        {
            if (LeftTrigger.GetAxis(SteamVR_Input_Sources.Any) > sensitivity)
            {
                speed = LeftHandPos.transform.rotation * Vector3.forward * maxSpeed * LeftTrigger.GetAxis(SteamVR_Input_Sources.Any);
                speed = new Vector3(speed.x, 0, speed.z);
            }
            else if (RightTrigger.GetAxis(SteamVR_Input_Sources.Any) > sensitivity)
            {
                speed = RightHandPos.transform.rotation * Vector3.forward * maxSpeed * RightTrigger.GetAxis(SteamVR_Input_Sources.Any);
                speed = new Vector3(speed.x, 0, speed.z);
            }
            else
            {
                speed = Vector3.zero;
            }
            transform.localPosition += speed;
            //May need to move tablets too for developing use
        }

    }

}