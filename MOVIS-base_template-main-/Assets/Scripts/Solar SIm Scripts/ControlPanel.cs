using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public GameObject OrbitScaleLever;
    public GameObject PlanetScaleLever;
    public GameObject speedLever;
    public GameObject ArrowSwitch;

    private float maxLever = 1.4f;
    private float leverSpeed = 0.008f;
    //private float maxSwitch = 55f;

    //private bool isReleased = true;

    // Update is called once per frame
    void Update()
    {
        //leverUpdate();
        //switchUpdate();
    }

    void switchUpdate()
    {
        //Debug.Log(Input.GetKey(KeyCode.Space) + " " + isReleased);
        /*
        if (isReleased && Input.GetKey(KeyCode.Space))
        {
            UniverseController.showArrows = !UniverseController.showArrows;
            foreach(PlanetIdentifier pi in FindObjectsOfType<PlanetIdentifier>())
            {
                pi.updateVisability();
            }
            isReleased = false;
        }
        else if(!Input.GetKey(KeyCode.Space))
        {
            isReleased = true;
        }
        ArrowSwitch.transform.localEulerAngles = new Vector3((UniverseController.showArrows ? -1 : 1) * maxSwitch, 0, 0);*/
    }

    void leverUpdate()
    {
        if(lever(KeyCode.UpArrow, KeyCode.DownArrow, OrbitScaleLever))
        {
            foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
            {
                //pc.drawTrajectory();
            }
        }
        if(lever(KeyCode.Z, KeyCode.C, PlanetScaleLever))
        {
            foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
            {
                pc.UpdateScale();
            }
        }
        if(lever(KeyCode.RightArrow, KeyCode.LeftArrow, speedLever))
        {
            foreach (PlanetController pc in FindObjectsOfType<PlanetController>())
            {
                //pc.drawTrajectory();
            }
        }
        //Maps the lever's position to the values
        UniverseController.orbitScale = 1.43f * OrbitScaleLever.transform.localPosition.y + 3;
        UniverseController.planetScale = 3.21f * PlanetScaleLever.transform.localPosition.y + 5.5f;
        UniverseController.orbitSpeedK = Mathf.RoundToInt(6.43f * speedLever.transform.localPosition.y + 10);
    }

    bool lever(KeyCode up, KeyCode down, GameObject lever)
    {
        if (Input.GetKey(up) && lever.transform.localPosition.y < maxLever)
        {
            lever.transform.Translate(new Vector3(0, leverSpeed, 0));
            if (lever.transform.localPosition.y > maxLever)
            {
                lever.transform.localPosition = new Vector3(0, maxLever, 0);
            }
            return true;
        }
        else if (Input.GetKey(down) && lever.transform.localPosition.y > -maxLever)
        {
            lever.transform.Translate(new Vector3(0, -leverSpeed, 0));
            if (lever.transform.localPosition.y < -maxLever)
            {
                lever.transform.localPosition = new Vector3(0, -maxLever, 0);
            }
            return true;
        }
        return false;
    }
}
