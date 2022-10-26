using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshScaler : MonoBehaviour
{
    private Vector3[] scales = {new Vector3(0.000003476238745f, 0.000003476238745f, 0.000003476238745f),
        new Vector3(0.05079997257f, 0.05079997257f, 0.05079997257f),
        new Vector3(190.893109f, 190.893109f, 190.893109f)};
    public int view = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * scales[view - 1].x * UniverseController.planetScale;
    }
}
