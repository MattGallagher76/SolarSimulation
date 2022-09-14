//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: For controlling in-game objects with tracked devices.
//
//=============================================================================

using UnityEngine;
using Valve.VR;


namespace Valve.VR
{
    public class SteamVR_TrackedObject : MonoBehaviour
    {
        public enum EIndex
        {
            None = -1,
            Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15,
            Device16
        }


        public EIndex index;

        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public bool isValid { get; private set; }

        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {
            if (index == EIndex.None)
                return;

            var i = (int)index;

            isValid = false;
            if (poses.Length <= i)
                return;

            if (!poses[i].bDeviceIsConnected)
                return;

            if (!poses[i].bPoseIsValid)
                return;

            isValid = true;

            var pose = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);

            if (origin != null)
            {
                transform.position = origin.transform.TransformPoint(pose.pos);
                transform.rotation = origin.rotation * pose.rot;
            }
            else
            {
                transform.localPosition = pose.pos;
                transform.localRotation = pose.rot;
            }
        }

        SteamVR_Events.Action newPosesAction;

        SteamVR_TrackedObject()
        {
            newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
        }

        private void Awake()
        {
            OnEnable();
        }

        void OnEnable()
        {
            var render = SteamVR_Render.instance;
            if (render == null)
            {
                enabled = false;
                return;
            }

            newPosesAction.enabled = true;
        }

        void OnDisable()
        {
            newPosesAction.enabled = false;
            isValid = false;
        }

        public void SetDeviceIndex(int index)
        {
            if (System.Enum.IsDefined(typeof(EIndex), index))
                this.index = (EIndex)index;
        }

        #region TrackerAutoMaticSetting

        //DO NOT FIX THIS UNLESS YOU ARE REPLACING THE TRACKERS!!!!
        public string Tracker1ID = "LHR-6E6212A4";
        public string Tracker6ID = "LHR-A36C3E16";
        public string Tracker8ID = "LHR-4780977C";
        public string Tracker9ID = "LHR-4F225E25";

        private void Start()
        {
            TrackerSetup();
        }

        void TrackerSetup()
        {
            ETrackedPropertyError error = new ETrackedPropertyError();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i <= 16; i++) //check all device from 0 - 16;
            {
                OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
                var SerialNumber = sb.ToString(); //it will extract the tracker number.

                //Below is where you need to identify the tracker and setup.

                if (SerialNumber == Tracker1ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("1")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker6ID) //if extracted traker number is matched with the TrakcerID,
                {
                    if (this.gameObject.name.Contains("6")) //AND, it matches with prefab ID,
                    {
                        SetDeviceIndex(i); //Set to designated device number.
                        break;
                    }
                }

                if (SerialNumber == Tracker8ID)
                {
                    if (this.gameObject.name.Contains("8"))
                    {
                        SetDeviceIndex(i);
                        break;
                    }
                }
                if (SerialNumber == Tracker9ID)
                {
                    if (this.gameObject.name.Contains("9"))
                    {
                        SetDeviceIndex(i);
                        break;
                    }
                }

            }
        }
        #endregion
    }



}