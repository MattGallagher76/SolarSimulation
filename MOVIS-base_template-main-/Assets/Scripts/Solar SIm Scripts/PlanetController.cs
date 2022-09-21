using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public float diameter;
    public float mass;
    public Vector3 InitialPosition;
    public Vector3 InitialVelocity;
    public float tiltAngle;
    public float rotationSpeed;
    public float privateOrbitScale;
    public bool isPined;
    public GameObject mesh;
    public int ID;

    public Vector3 MathPosition { get; set; }
    public Vector3 Velocity { get; set; }
    public VirtualController controller { get; set; }
    public float[][] ViewTypeChangeMatrix { get; set; }

    //private LinkedList<Vector3> trajectoryList;
    /*
     * MathPos should be used for any velocity, position, or acceleration calculations and is not affected by scale
     * Rigidbody's position values should change due to scale and are not used in any calulations. 
     */

    void Awake()
    {
        Velocity = InitialVelocity;
        mesh.transform.localScale = Vector3.one * diameter; //Could move to editmode script if needed
        transform.localPosition = InitialPosition;
        MathPosition = transform.localPosition * privateOrbitScale;
        transform.eulerAngles = new Vector3(0, 0, tiltAngle);
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void updateLocation()
    {
        if (!isPined)
        {
            MathPosition = controller.points.First.Value;
            //Debug.Log(ID);
            //Debug.Log(MathPosition);
            //Debug.Log(GetComponentInParent<UniverseController>().cameraLockedPlanet.controller.points.First.Value);
            transform.localPosition = (MathPosition * UniverseController.orbitScale * privateOrbitScale) - GetComponentInParent<UniverseController>().cameraLockedPlanet.controller.points.First.Value;
        }
        else
        {
            //transform.localPosition = GetComponentInParent<UniverseController>().cameraLockedPlanet.InitialPosition;
        }
        transform.eulerAngles += new Vector3(0, rotationSpeed / UniverseController.orbitSpeedK, 0);
    }

    public void UpdateScale()
    {
        mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale;
    }

    public void changeViewType(int ViewType)
    {
        PlanetData pd = GetComponentInParent<PlanetData>();
        float[][] changeMatrix = new float[2][];
        for (int i = 0; i < changeMatrix.Length; i++)
        {
            changeMatrix[i] = new float[PlanetData.changeDuration];
        }
        for(int i = 0; i < PlanetData.changeDuration; i ++)
        {
            changeMatrix[0][i] = (i) * (pd.PlanetList[ID].Diameter[ViewType - 1] - diameter) / (PlanetData.changeDuration - 1) + diameter;
            changeMatrix[1][i] = (i) * (pd.PlanetList[ID].OrbitScale[ViewType - 1] - privateOrbitScale) / (PlanetData.changeDuration - 1) + privateOrbitScale;
        }
        ViewTypeChangeMatrix = changeMatrix;
    }

    public void UpdateChangeValues()
    {
        mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale;
        if(!isPined)
            transform.localPosition = (MathPosition * UniverseController.orbitScale * privateOrbitScale);
    }
}
