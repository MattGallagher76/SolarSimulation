using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is used to handle the arrows above the planets during view type 1
 */
public class PlanetIdentifier : MonoBehaviour
{

    private float index = 0f; //Used to simulate a slight bobbing up and down 
    private float initY = 0f; //Used to simulate a slight bobbing up and down

    public float ArrowScale = 0.3f; //Scale of arrows
    public float ArrowBobMagnitude = 0f; //How much the arrows move
    public float ArrowOffset = 0.3f; //How high off the planets the arrows move
    public bool showArrows = true; //If arrows are currently showing
    public GameObject marker; //The position of the tip of the arrow

    // Start method
    void Start()
    {
        initArrow();
    }

    // Update method
    void Update()
    {
        if (showArrows)
        {
            index += Time.deltaTime;
            float y = ArrowBobMagnitude / 2 * Mathf.Sin(index);
            transform.localPosition = new Vector3(transform.localPosition.x, initY + y, transform.localPosition.z);
        }
    }

    /*
     * Sets the arrows up
     */
    public void initArrow()
    {
        transform.localScale = Vector3.one * ArrowScale;
        float yOffset = (marker.transform.localPosition.y * -ArrowScale) + GetComponentInParent<PlanetController>().diameter / 2 + ArrowOffset;// + GetComponentInParent<PlanetController>().InitialPosition.y;
        transform.localPosition = new Vector3(0, yOffset, 0);
        initY = yOffset;
    }

    /*
     * Used to update visability
     */
    public void updateVisability()
    {
        if(showArrows)
        {
            showArrow();
        }
        else
        {
            hideArrow();
        }
    }

    //Hides arrows
    public void hideArrow()
    {
        transform.localScale = Vector3.zero;
    }

    //Shows arrows
    public void showArrow()
    {
        transform.localScale = Vector3.one * ArrowScale;
    }
}
