using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualController
{
    public Vector3 position; //Acts like mathPos of CelestialController
    public Vector3 velocity;
    public float mass;
    public bool isPined;

    public LinkedList<Vector3> points = new LinkedList<Vector3>();

    public VirtualController(PlanetController parent)
    {
        velocity = parent.InitialVelocity;
        position = parent.transform.localPosition;
        mass = parent.mass;
        isPined = parent.isPined;
    }

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
