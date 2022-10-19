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

    public void updateLocation()
    {
        MathPosition = controller.points.First.Value;
        transform.localPosition = (MathPosition - GetComponentInParent<UniverseController>().cameraLockedPlanet.controller.points.First.Value) * UniverseController.orbitScale * privateOrbitScale;
        /*
        if (!isPined)
        {
            MathPosition = controller.points.First.Value;
            transform.localPosition = (MathPosition * UniverseController.orbitScale * privateOrbitScale) - GetComponentInParent<UniverseController>().cameraLockedPlanet.controller.points.First.Value;
        }
        else
        {
            //transform.localPosition = GetComponentInParent<UniverseController>().cameraLockedPlanet.InitialPosition;
        }
        */
        //transform.eulerAngles += new Vector3(0, rotationSpeed / UniverseController.orbitSpeedK, 0);
    }

    public void UpdateScale()
    {
        if (ID == 3)
        {
            Debug.Log("Diameter: " + diameter);
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale * 0.5f;
        }
        else
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale;
        }
    }
    
    public void changeViewType(int ViewType)
    {
        PlanetData pd = GetComponentInParent<PlanetData>();

        float[][] changeMatrix = new float[2][];
        for (int i = 0; i < changeMatrix.Length; i++) //Sets up the changematrices
        {
            changeMatrix[i] = new float[UniverseController.changeDuration];
        }
        for (int i = 0; i < UniverseController.changeDuration; i++)
        {
            changeMatrix[0][i] = UniverseController.sigmoid(i) * (pd.PlanetList[ID].Diameter[ViewType - 1] - diameter) + diameter;
            changeMatrix[1][i] = UniverseController.sigmoid(i) * (pd.PlanetList[ID].OrbitScale[ViewType - 1] - privateOrbitScale) + privateOrbitScale;
        }
        changeMatrix[0][UniverseController.changeDuration - 1] = pd.PlanetList[ID].Diameter[ViewType - 1];
        changeMatrix[1][UniverseController.changeDuration - 1] = pd.PlanetList[ID].OrbitScale[ViewType - 1];
        ViewTypeChangeMatrix = changeMatrix;
    }

    public void UpdateChangeValues()
    {
        if (ID == 3)
        {
            Debug.Log("Diameter: " + diameter);
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale * 0.5f;
        }
        else
        {
            mesh.transform.localScale = Vector3.one * diameter * UniverseController.planetScale;
        }
        if (!isPined)
            transform.localPosition = (MathPosition * UniverseController.orbitScale * privateOrbitScale);
    }
}
