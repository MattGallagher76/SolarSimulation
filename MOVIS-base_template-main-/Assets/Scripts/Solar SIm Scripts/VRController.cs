using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class VRController : MonoBehaviour
{
    public float sensitivity;
    public float maxSpeed;

    public SteamVR_Action_Boolean leftChangeScene = null;
    public SteamVR_Action_Boolean rightChangeScene = null;
    public SteamVR_Action_Single rightTrigger = null;
    public SteamVR_Action_Single leftTrigger = null;

    public GameObject LeftHandPos;
    public GameObject RightHandPos;

    public GameObject LeftHandCol;
    public GameObject RightHandCol;

    public GameObject ViewTypeMenuPrefab;

    [SerializeField]
    private GameObject rightHandTrigger;
    [SerializeField]
    private GameObject leftHandTrigger;

    private Vector3 speed = Vector3.zero;

    private CharacterController charController = null;
    private Transform cameraRig = null;
    private Transform head = null;
    private List<GameObject> menuItems;

    private static bool hasStateChanged = false;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
        menuItems = new List<GameObject>();
    }

    private void Update()
    {
        //CallObserver();
        HandleHead(); //Movement and avatar mangement
        HandleHeight();
        CalcMovement();


        if (leftChangeScene.state && !hasStateChanged)
        {
            for (int i = 0; i < PlanetData.ViewTypeCount; i++)
            {
                GameObject view = Instantiate(ViewTypeMenuPrefab, LeftHandPos.transform.position + LeftHandPos.transform.rotation * (new Vector3((i * 0.2f) - 0.1f, 0.1f, 0)), transform.rotation);
                view.GetComponent<ViewTypeMenu>().ViewTypeID = i + 1;
                view.GetComponent<ViewTypeMenu>().controller = this;
                menuItems.Add(view);
            }
            hasStateChanged = !hasStateChanged;
        }

        
        if(leftChangeScene.state && hasStateChanged == false)
        {
            UniverseController.orbiting = false;
            foreach(PlanetController pc in FindObjectsOfType<PlanetController>())
            {
                pc.changeViewType(2);
            }
            hasStateChanged = true;
        }
    }

    public void changeScene(int scene)
    {
        if(scene == 1)
        {
            foreach(PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                pi.showArrow();
            }
        }
        else
        {
            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                pi.hideArrow();
            }
        }
        UniverseController.orbiting = false;
        foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
        {
            pc.changeViewType(scene);
        }

        foreach (GameObject gb in menuItems)
        {
            Destroy(gb);
        }
    }

    private void HandleHead()
    {
        Vector3 oldPos = cameraRig.position;
        Quaternion oldRot = cameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        cameraRig.position = oldPos;
        cameraRig.rotation = oldRot;
    }

    private void CalcMovement()
    {
        if (leftTrigger.GetAxis(SteamVR_Input_Sources.Any) > sensitivity)
        {
            speed = LeftHandPos.transform.rotation * Vector3.forward * maxSpeed * leftTrigger.GetAxis(SteamVR_Input_Sources.Any);
            speed = new Vector3(speed.x, 0, speed.z);
        }
        else if (rightTrigger.GetAxis(SteamVR_Input_Sources.Any) > sensitivity)
        {
            speed = RightHandPos.transform.rotation * Vector3.forward * maxSpeed * rightTrigger.GetAxis(SteamVR_Input_Sources.Any);
            speed = new Vector3(speed.x, 0, speed.z);
        }
        else
        {
            speed = Vector3.zero;
        }

        charController.Move(speed);
    }

    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        charController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = charController.height / 2;
        newCenter.y += charController.skinWidth;

        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        charController.center = newCenter;
    }

    public static void refreshSceneChange()
    {
        hasStateChanged = false;
    }
    //private void CallObserver()
    //{
    //    Vector3 updateObserver = new Vector3(leftTrigger.GetAxis(SteamVR_Input_Sources.Any), 0f, 0f);
    //    leftHandTrigger.transform.position = updateObserver;

    //    Vector3 updateObserver2 = new Vector3(rightTrigger.GetAxis(SteamVR_Input_Sources.Any), 0f, 0f);
    //    rightHandTrigger.transform.position = updateObserver2;
    //}
}
