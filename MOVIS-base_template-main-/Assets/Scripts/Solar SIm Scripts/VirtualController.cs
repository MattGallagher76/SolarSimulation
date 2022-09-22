using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All planets have a VirtualController within. This class does not have a 3D presence in the world, they are simply used to store and calculate
 * information regarding the orbit of the planets.
 */
public class VirtualController
{
    public Vector3 position; //Acts like mathPos of CelestialController
    public Vector3 velocity; //Stores the theoretical velocity
    public float mass; //Stores the mass
    public bool isPined; //Used to pin planets in place (used for the sun) This should eventually be phased out.

    public LinkedList<Vector3> points = new LinkedList<Vector3>(); //A linked list of all the future positions the planet will move

    /*
     * Constructor used to take in a planet it is representing and saves all the data.
     */
    public VirtualController(PlanetController parent)
    {
        velocity = parent.InitialVelocity;
        position = parent.transform.localPosition;
        mass = parent.mass;
        isPined = parent.isPined;
    }

    /*
     * Calculates the velocity of the planet given all the other planets and a deltaTime. 
     */
    public Vector3 CalculateVelocity(VirtualController[] bodies, float deltaTime)
    {
        if (!isPined)
        {
            Vector3 vel = velocity;
            foreach (VirtualController vc in bodies)
            {
                if (vc != this)
                {
                    float distanceSquared = (vc.position - position).sqrMagnitude;
                    Vector3 forceDirection = (vc.position - position).normalized;
                    Vector3 acc = forceDirection * UniverseController.bigG * vc.mass / distanceSquared;
                    vel += acc * deltaTime;
                }
            }
            return vel;
        }
        return Vector3.zero;
    }


}
