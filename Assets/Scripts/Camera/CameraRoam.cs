using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    public float cameSpeed = 20;
    public float screenSizeThickens = 10;


    [SerializeField] private float minCityX;
    [SerializeField] private float maxCityX;
    [SerializeField] private float minCityZ;
    [SerializeField] private float maxCityZ;


    [SerializeField] private float minWildX;
    [SerializeField] private float maxWildX;
    [SerializeField] private float minWildZ;
    [SerializeField] private float maxWildZ;

    [SerializeField] private Teleporter teleporter;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (teleporter.isInCity)
        {
            Debug.Log("in city");
            pos.x = Mathf.Clamp(pos.x, minCityX, maxCityX);
            pos.z = Mathf.Clamp(pos.z, minCityZ, maxCityZ);
        }
        if (!teleporter.isInCity)
        {
            Debug.Log("out of city");
            pos.x = Mathf.Clamp(pos.x, minWildX, maxWildX);
            pos.z = Mathf.Clamp(pos.z, minWildZ, maxWildZ);
        }

        //Up
        if (Input.mousePosition.y >= Screen.height - screenSizeThickens)
        {
            pos.x -= cameSpeed * Time.deltaTime;
        }


        //Down
        if (Input.mousePosition.y <= screenSizeThickens)
        {
            pos.x += cameSpeed * Time.deltaTime;
        }

        //Right
        if (Input.mousePosition.x >= Screen.height - screenSizeThickens)
        {
            pos.z += cameSpeed * Time.deltaTime;
        }

        //Left
        if (Input.mousePosition.x <= screenSizeThickens)
        {
            pos.z -= cameSpeed * Time.deltaTime;
        }
        pos.y = 9.8f;
        transform.localPosition = pos;
    }
}
