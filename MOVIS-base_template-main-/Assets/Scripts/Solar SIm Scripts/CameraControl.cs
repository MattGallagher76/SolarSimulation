using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed;
    public float mouseSpeed;
    public float cameraMaxAngle;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Camera.main.transform.right * -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Camera.main.transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Camera.main.transform.forward * -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
        }
        transform.eulerAngles += mouseSpeed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }
}
