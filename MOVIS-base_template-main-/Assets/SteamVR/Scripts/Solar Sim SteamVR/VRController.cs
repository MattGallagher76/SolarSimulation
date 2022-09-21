using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;

namespace Valve.VR
{

    public class VRController : MonoBehaviour 
    {
        public float sensitivity;
        public float maxSpeed;

        public SteamVR_Action_Boolean LeftChangeViewType = null;
        public SteamVR_Action_Boolean RightChangeViewType = null;
        public SteamVR_Action_Single RightTrigger = null;
        public SteamVR_Action_Single LeftTrigger = null;

        public GameObject LeftHandPos;
        public GameObject RightHandPos;

        public GameObject LeftHandCol;
        public GameObject RightHandCol;

        public GameObject ViewTypeMenuPrefab;
        public GameObject ViewTypeNetworker;

        private Vector3 speed = Vector3.zero;

        public bool isChanging = false;
        private int ViewTypeCount = 2;//If adding a viewtype, change this value AND the one in PlanetData
        private GameObject[] menuItems;

        private void Start()
        {
            menuItems = new GameObject[ViewTypeCount];
        }

        private void Update()
        {
            CalcMovement();//Only for testing, remove for demo.
            CheckMenu();
        }

        private void CheckMenu()
        {
            if(LeftChangeViewType.state && !isChanging)
            {
                isChanging = true;
                for(int i = 0; i < ViewTypeCount; i ++)
                {
                    GameObject view = Instantiate(ViewTypeMenuPrefab, LeftHandPos.transform.position + LeftHandPos.transform.rotation * (new Vector3((i * 0.2f) - (ViewTypeCount * 0.1f / 2), 0.1f, 0)), transform.rotation);
                    view.GetComponent<ViewTypeMenu>().ViewTypeID = i + 1;
                    view.GetComponent<ViewTypeMenu>().controller = this;
                    menuItems[i] = view;
                }
            }
            else if (RightChangeViewType.state && !isChanging)
            {
                isChanging = true;
                for (int i = 0; i < ViewTypeCount; i++)
                {
                    GameObject view = Instantiate(ViewTypeMenuPrefab, RightHandPos.transform.position + RightHandPos.transform.rotation * (new Vector3((i * 0.2f) - (ViewTypeCount * 0.1f / 2), 0.1f, 0)), transform.rotation);
                    view.GetComponent<ViewTypeMenu>().ViewTypeID = i + 1;
                    view.GetComponent<ViewTypeMenu>().controller = this;
                    menuItems[i] = view;
                }
            }
        }

        public void removeMenu()
        {
            foreach(GameObject menu in menuItems)
            {
                Destroy(menu);
            }
        }

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