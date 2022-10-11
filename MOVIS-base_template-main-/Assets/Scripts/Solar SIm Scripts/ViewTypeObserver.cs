using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for taking in information from the VR user, and telling all the builds to do the same action at the same time.
 */
public class ViewTypeObserver : MonoBehaviour
{
    public int currentViewType = 0; //The current viewtype that the scene is displaying
    public PlanetController earth;

    /*
     * Changes the viewtype of all devices.
     */
    public void changeView(int scene)
    {
        if (scene == 1) //View type 1 needs arrows, the rest of the viewtypes currently do not
        {
            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                pi.showArrow();
            }
        }
        else
        {
            foreach (PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                //pi.hideArrow();
            }
        }
        if (scene == 3)
        {
            FindObjectOfType<UniverseController>().cameraLockedPlanet = earth;
        }
        UniverseController.orbiting = false; //Stops the planets from orbiting and tells UniverseController to change the planets data for the given scene.
        foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
        {
            pc.changeViewType(scene);
        }
    }

    // Update Method
    void Update()
    {
        if(transform.localPosition.x != currentViewType)
        {
            currentViewType = (int)transform.localPosition.x;
            changeView(currentViewType);
            Debug.Log("Changing to viewtype " + currentViewType + 1);
        }
    }
}
