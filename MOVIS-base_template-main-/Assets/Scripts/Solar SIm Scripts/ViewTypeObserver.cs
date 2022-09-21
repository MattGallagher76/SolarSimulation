using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTypeObserver : MonoBehaviour
{
    public int currentViewType = 0;

    public void changeScene(int scene)
    {
        if (scene == 1)
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
                pi.hideArrow();
            }
        }
        UniverseController.orbiting = false;
        foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
        {
            pc.changeViewType(scene);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x != currentViewType)
        {
            currentViewType = (int)transform.localPosition.x;
            changeScene(currentViewType);
            Debug.Log("Changing to viewtype " + currentViewType + 1);
        }
    }
}
