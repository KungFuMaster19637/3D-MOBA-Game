using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRotate : MonoBehaviour
{
    public Transform icon;
    void Update()
    {
        icon.eulerAngles = new Vector3(icon.eulerAngles.x, -90, icon.eulerAngles.z);
    }
}
