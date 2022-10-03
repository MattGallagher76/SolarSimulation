using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : MonoBehaviour
{
    public static float bigG = 6.67428f * Mathf.Pow(10, -11); //G used for gravitation force calculating
    public static float planetScale = 1; //The scale planets are displayed
    public static float orbitScale = 1; //The scale of all orbits - can be used to scale the entire system at once rather than each individually

    public static int steps = 100; //How many steps the orbit of planets is calculated ahead of time. Affects the maximum speed.
    public static float timeStep = 0.001f; //The frequency which the planets position is calculated
    public static int orbitSpeedK = 10; //The rate at which planets step through the points list
    public static int displayK = 30; //The rate at which line renderers step through the points list

    public static bool orbiting = true; //Used to determine if planets are orbiting or changing view type
    public static int changeSteps = 0; //Used while changing view types

    private PlanetController[] Planets; //List of planets to reference
    private VirtualController[] Bodies; //List of the virtual controlles in the planets to reference on build

    public static float SigmoidK = 14;
    public static float SigmoidOffset;
    public static float SigmoidOffsetDelta = 0.1f;
    public static float tolerance = 0.001f;
    public static int changeDuration = 1000;

    public PlanetController cameraLockedPlanet; //Which ever planet is currently locked to the camera view. This should replace a pined planet. WIP

    private int k = 0;
    /*
     * Sets up all planets and virtual controllers
     */
    private void Awake()
    {
        Planets = FindObjectsOfType<PlanetController>();
        Bodies = new VirtualController[Planets.Length];
        for(int i = 0; i < Planets.Length; i ++)
        {
            Bodies[i] = new VirtualController(Planets[i]);
        }
        InitiateVirtualControllers();
        for (int i = 0; i < Planets.Length; i++)
        {
            Planets[i].controller = Bodies[i];
            Planets[i].mesh.transform.localPosition = Vector3.zero;
        }
    }

    /*
     * Shows all arrows, can be removed eventually (I think)
     */
    private void Start()
    {
        while(sigmoid(0) > tolerance)
        {
            SigmoidOffset += SigmoidOffsetDelta;
        }
        foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
        {
            pi.updateVisability();
        }
    }

    public static float sigmoid(float x)
    {
        return 1 / (1 + Mathf.Pow((float)System.Math.E, -1 * SigmoidK * (x/changeDuration - SigmoidOffset)));
    }

    /*
     * The Update method that handles moving the planets or changing viewtypes
     */
    void Update()
    {
        k++;
        if(k == 1000)
        {
            FindObjectOfType<ViewTypeObserver>().changeView(2);
        }
        if(!LobbyManager.userType)
        {
            if(orbiting)
            {
                foreach (PlanetController pc in Planets)
                {
                    pc.updateLocation();
                }
                updateVirtualControllers();
                if (changeSteps != 0)
                {
                    changeSteps = 0;
                }
            }
            else //Changing viewtypes
            {
                foreach (PlanetController pc in Planets)
                {
                    pc.diameter = pc.ViewTypeChangeMatrix[0][changeSteps];
                    pc.privateOrbitScale = pc.ViewTypeChangeMatrix[1][changeSteps];
                    pc.UpdateChangeValues();
                }
                if (changeSteps == changeDuration - 1)
                {
                    orbiting = true;
                }
                changeSteps++;
            }
        }
    }

    /*
     * To use only if each point's list needs to be completely remade. A change in orbit speed or scale does not need a new points list
     */
    public void InitiateVirtualControllers()
    {
        foreach (VirtualController vc in Bodies) //If used while points lists are full, clears them
        {
            vc.points.Clear();
        }
        for (int i = 0; i < steps; i++)
        {
            for (int j = 0; j < Bodies.Length; j++) //Sets new Velocities
            {
                if (!Bodies[j].isPined)
                {
                    Bodies[j].velocity = Bodies[j].CalculateVelocity(Bodies, timeStep);
                }
            }
            for (int j = 0; j < Bodies.Length; j++)
            {
                if (!Bodies[j].isPined)
                {
                    Vector3 newPos = Bodies[j].position + Bodies[j].velocity * timeStep;
                    Bodies[j].position = newPos;
                    Bodies[j].points.AddLast(newPos); //Points list does not change when orbit scale is changed. That should occur within PlanetController
                }
                else
                {
                    Bodies[j].points.AddLast(Bodies[j].position);
                }
            }
        }
    }

    /*
     * Used to find the next orbitSpeedK spaces in points
     */
    public void updateVirtualControllers()
    {
        foreach(VirtualController vc in Bodies) //Removes the index that planets should currently be at
        {
            for (int i = 0; i < orbitSpeedK; i++)
            {
                vc.points.RemoveFirst();
            }
        }

        for(int i = 0; i < orbitSpeedK; i ++)
        {
            for (int j = 0; j < Bodies.Length; j++)
            {
                Bodies[j].velocity = Bodies[j].CalculateVelocity(Bodies, timeStep);
            }
            for (int j = 0; j < Bodies.Length; j++)
            {
                Vector3 newPos = Bodies[j].position + Bodies[j].velocity * timeStep;
                Bodies[j].position = newPos;
                Bodies[j].points.AddLast(newPos);
            }
        }
    }
}
