using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR
{
    public class SteamVRMenuItem : MonoBehaviour
    {
        public float maxRot = 135f;
        public int TypeID; //0 = Knob, 1 = 3D Rotational Object 
        public int ObjectID; //Specific to the exact Object
        public SteamVRController controller; //The script that created this menu item
        public bool isGrabbing;

        // Update is called once per frame
        void Update()
        {
            if(TypeID == 0)
            {
                if(ObjectID == 0) //OrbitSpeedK
                {
                    float slope = 20f / (2 * maxRot); //Should allow for the speed to be between 0 and 20
                    if(controller.RightButton.state && isGrabbing)
                    {
                        transform.eulerAngles = controller.RightHandPos.transform.eulerAngles;
                    }
                    if(transform.eulerAngles.x <= -1*maxRot)
                    {
                        transform.eulerAngles = new Vector3(-1 * maxRot, 0f, 0f);
                    }
                    if (transform.eulerAngles.x >= maxRot)                                                      
                    {
                        transform.eulerAngles = new Vector3(maxRot, 0f, 0f);
                    }
                    float xTemp = controller.MenuItemNetworker.transform.localPosition.x * slope + 10f;
                    float yTemp = controller.MenuItemNetworker.transform.localPosition.y;
                    float zTemp = controller.MenuItemNetworker.transform.localPosition.z;
                    controller.MenuItemNetworker.transform.localPosition = new Vector3(xTemp, yTemp, zTemp);
                }
            }
            if(TypeID == 1)
            {
                if(ObjectID == 0) //Universe Center Rotator, mainly for viewtype 3
                {
                    if(controller.RightButton.state && isGrabbing)
                    {
                        controller.MenuItemNetworker.transform.eulerAngles = controller.RightHandPos.transform.eulerAngles;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            isGrabbing = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isGrabbing = false;
        }
    }
}
