using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetIdentifier : MonoBehaviour
{

    private float index = 0f;
    private float initY = 0f;

    public float ArrowScale = 0.3f;
    public float ArrowBobMagnitude = 0f;//1.5f;
    public float ArrowOffset = 0.3f;
    public bool showArrows = true;
    public GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        initArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (showArrows)
        {
            index += Time.deltaTime;
            float y = ArrowBobMagnitude / 2 * Mathf.Sin(index);
            transform.localPosition = new Vector3(transform.localPosition.x, initY + y, transform.localPosition.z);
        }
    }

    public void initArrow()
    {
        /*
        transform.localScale = Vector3.one * ArrowScale;
        float yOffset = (ArrowBobMagnitude / 2) + GetComponentInParent<PlanetController>().diameter / 2 + ArrowOffset;
        transform.localPosition = new Vector3(0, yOffset, 0) + GetComponentInParent<PlanetController>().mesh.transform.localPosition; //Adding the mesh local position is only needed in edit mode. Once started the localPos is zeroed.
        initY = yOffset;
        */
        transform.localScale = Vector3.one * ArrowScale;
        float yOffset = (marker.transform.localPosition.y * -ArrowScale) + GetComponentInParent<PlanetController>().diameter / 2 + ArrowOffset;// + GetComponentInParent<PlanetController>().InitialPosition.y;
        transform.localPosition = new Vector3(0, yOffset, 0);
        initY = yOffset;
    }

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

    public void hideArrow()
    {
        transform.localScale = Vector3.zero;
    }

    public void showArrow()
    {
        transform.localScale = Vector3.one * ArrowScale;
    }
}
