using UnityEngine;

public class FanRotator : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
