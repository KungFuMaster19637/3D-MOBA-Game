using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{
    public float camSpeed = 20;
    public float screenSizeThickens = 10;

    [Header ("Castle Camera Limits")]
    [SerializeField] private float minCastleX;
    [SerializeField] private float maxCastleX;
    [SerializeField] private float minCastleZ;
    [SerializeField] private float maxCastleZ;

    [Header("Wild Camera Limits")]
    [SerializeField] private float minWildX;
    [SerializeField] private float maxWildX;
    [SerializeField] private float minWildZ;
    [SerializeField] private float maxWildZ;

    [Header("Cave Camera Limits")]
    [SerializeField] private float minCaveX;
    [SerializeField] private float maxCaveX;
    [SerializeField] private float minCaveZ;
    [SerializeField] private float maxCaveZ;

    /*
    Cam coordinates:
    Castle:
    x: -75 / -115
    z:  -18 / 18

    Wild:
    x: -80 / -180
    z: -220 / -360

    Cave:
    x: -70 / -250
    z: -670 / -900

    */

    [SerializeField] private Teleporter teleporter;
    [SerializeField] private Teleporter2 teleporter2;

    void Update()
    {
        Vector3 pos = transform.position;

        if (teleporter.isInCastle)
        {
            pos.x = Mathf.Clamp(pos.x, minCastleX, maxCastleX);
            pos.z = Mathf.Clamp(pos.z, minCastleZ, maxCastleZ);
        }
        if (!teleporter.isInCastle && !teleporter2.isInCave)
        {
            pos.x = Mathf.Clamp(pos.x, minWildX, maxWildX);
            pos.z = Mathf.Clamp(pos.z, minWildZ, maxWildZ);
        }
        if (teleporter2.isInCave)
        {
            pos.x = Mathf.Clamp(pos.x, minCaveX, maxCaveX);
            pos.z = Mathf.Clamp(pos.z, minCaveZ, maxCaveZ);
        }

        //Up
        if (Input.mousePosition.y >= Screen.height - screenSizeThickens)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }


        //Down
        if (Input.mousePosition.y <= screenSizeThickens)
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        //Right
        if (Input.mousePosition.x >= Screen.height - screenSizeThickens)
        {
            pos.z += camSpeed * Time.deltaTime;
        }

        //Left
        if (Input.mousePosition.x <= screenSizeThickens)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }

        //Y position locked
        pos.y = 9.8f;
        transform.localPosition = pos;
    }
}
