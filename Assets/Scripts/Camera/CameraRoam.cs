using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    public float cameSpeed = 20;
    public float screeSizeThickens = 10;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        //Up
        if (Input.mousePosition.y >= Screen.height - screeSizeThickens)
        {
            pos.x -= cameSpeed * Time.deltaTime;
        }


        //Down
        if (Input.mousePosition.y <= screeSizeThickens)
        {
            pos.x += cameSpeed * Time.deltaTime;
        }

        //Right
        if (Input.mousePosition.x >= Screen.height - screeSizeThickens)
        {
            pos.z += cameSpeed * Time.deltaTime;
        }

        //Left
        if (Input.mousePosition.x <= screeSizeThickens)
        {
            pos.z -= cameSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
