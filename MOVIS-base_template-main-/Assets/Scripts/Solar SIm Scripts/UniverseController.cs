using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : MonoBehaviour
{

    public static float bigG = 6.67428f * Mathf.Pow(10, -11);
    public static float planetScale = 1;
    public static float orbitScale = 1;
    //public static float width = 0.05f;

    public static float displayedPlanetYMultiplier = 1;

    public static int steps = 100; //Temp Val
    public static float timeStep = 0.001f; //The frequency which the planets position is calculated
    public static int orbitSpeedK = 10; //The rate at which planets step through the points list
    public static int displayK = 30; //The rate at which line renderers step through the points list

    public static bool orbiting = true;
    public static int ViewType = 0;
    public static int changeSteps = 0;

    private PlanetController[] Planets;
    private VirtualController[] Bodies;

    public PlanetController cameraLockedPlanet;
    //private static bool hasStateChanged = false;

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

    private void Start()
    {
        foreach(PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
        {
            pi.updateVisability();
        }
    }

    void Update()
    {
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
                if (changeSteps == PlanetData.changeDuration - 1)
                {
                    orbiting = true;
                }
                changeSteps++;
            }
        }
    }

    public void InitiateVirtualControllers() //To use only if each point's list needs to be completely remade. A change in orbit speed or scale does not need a new points list
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

    public void updateVirtualControllers() //Used to find the next orbitSpeedK spaces in points
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

    void editModeArrows()
    {
        foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
        {
            pi.updateVisability();
            pi.initArrow();
        }
    }
}
