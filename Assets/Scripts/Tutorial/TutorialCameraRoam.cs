using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraRoam : MonoBehaviour
{
    public float camSpeed = 20;
    public float screenSizeThickens = 10;

    [Header("Castle Camera Limits")]
    [SerializeField] private float minCastleX;
    [SerializeField] private float maxCastleX;
    [SerializeField] private float minCastleZ;
    [SerializeField] private float maxCastleZ;

    // Update is called once per frame
    private void Update()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minCastleX, maxCastleX);
        pos.z = Mathf.Clamp(pos.z, minCastleZ, maxCastleZ);

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
