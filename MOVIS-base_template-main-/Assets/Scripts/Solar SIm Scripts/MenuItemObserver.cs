using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemObserver : MonoBehaviour
{
    
    void Update()
    {
        UniverseController.orbitSpeedK = Mathf.RoundToInt(transform.localPosition.x);
        FindObjectOfType<UniverseController>().transform.eulerAngles = transform.eulerAngles;
    }
}
