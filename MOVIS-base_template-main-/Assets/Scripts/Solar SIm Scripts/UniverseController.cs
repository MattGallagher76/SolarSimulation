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
    public static int changeState = 0; //0 = Slowing down (default), 1 = changing orbit, 2 = speeding up, 3/0 = return to orbiting
    public static int currentSpeed = 0; //Current orbitSpeedK when changing

    private static TrailRenderer tr; //used to pause the trailrenderer when changing viewtypes
    public static float[] originalTime; //Will be used to store each planets time for trail renderer
    public static bool changed = false; //Not sure if actually needed but is here for now
    public static int count; //Created because of the way the for loops are done for now

    private PlanetController[] Planets; //List of planets to reference
    private VirtualController[] Bodies; //List of the virtual controlles in the planets to reference on build

    public static float SigmoidK = 14;
    public static float SigmoidOffset;
    public static float SigmoidOffsetDelta = 0.1f;
    public static float tolerance = 0.001f;
    public static int changeDuration = 1000;

    public PlanetController cameraLockedPlanet; //Which ever planet is currently locked to the camera view. This should replace a pined planet. WIP

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
        originalTime = new float[Planets.Length];
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
        //FindObjectOfType<ViewTypeObserver>().changeView(2);
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
        
        if(!LobbyManager.userType)
        {
            if(orbiting)
            {
                updateTrails();
                move();
                currentSpeed = orbitSpeedK;
            }
            else //Changing viewtypes
            {
                hideTrails();
                //Debug.Log(changeState + " " + changeSteps + " " + orbitSpeedK);
                if(changeState == 0) //Slowing down
                {
                    orbitSpeedK = Mathf.RoundToInt(currentSpeed * (1f - ((float)changeSteps)/500f));
                    move();
                    if(changeSteps == 500)
                    {
                        changeSteps = 0;
                        changeState = 1;
                    }
                    changeSteps++;
                }
                if(changeState == 1) //Changing
                {
                    foreach (PlanetController pc in Planets)
                    {
                        pc.diameter = pc.ViewTypeChangeMatrix[0][changeSteps];
                        pc.privateOrbitScale = pc.ViewTypeChangeMatrix[1][changeSteps];
                        pc.UpdateChangeValues();
                    }
                    if (changeSteps == changeDuration - 1)
                    {
                        changeState = 2;
                        changeSteps = 0;
                    }
                    changeSteps++;
                }
                if(changeState == 2) //Speeding up
                {
                    orbitSpeedK = Mathf.RoundToInt(currentSpeed * ((float)changeSteps) / 500f);
                    move();
                    if (changeSteps == 500)
                    {
                        changeSteps = 0;
                        changeState = 0;
                        orbiting = true;
                    }
                    changeSteps++;
                }
            }
        }
    }

    public void move()
    {
        foreach (PlanetController pc in Planets)
        {
            pc.updateLocation();
        }
        updateVirtualControllers();
    }

    public void updateTrails()
    {
        Debug.Log("UpdateTrails");
        count = 0;
        foreach (PlanetController pc in Planets)
        {
            if (changed)
            {
                tr = pc.GetComponent<TrailRenderer>();
                tr.time = originalTime[count];
            }
        }
        changed = false;
    }

    public void hideTrails()
    {
        Debug.Log("HideTrails");
        count = 0;
        foreach (PlanetController pc in Planets)
        {
            tr = pc.GetComponent<TrailRenderer>();
            if (tr.time != 0) //If the planet's time has not already been captured and changed to 0 previously
            {
                originalTime[count] = tr.time;
            }

            tr.time = 0;
        }
        changed = true;
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
