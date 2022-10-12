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
    public Light atmosphereLight;

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
        if (scene == 3)
        {
            atmosphereLight.intensity = 1.8f;
            FindObjectOfType<UniverseController>().cameraLockedPlanet = earth; //Eventually, I would like to make this transition look nice.
            FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0, -34.29f, 0);
        }
        else
        {
            atmosphereLight.intensity = 0f;
            FindObjectOfType<UniverseController>().gameObject.transform.position = new Vector3(0, 0, 0);
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
