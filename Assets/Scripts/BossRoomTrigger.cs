using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossRoomBlock;
    [SerializeField] private ItemDisplay itemDisplay;

    private void OnTriggerStay(Collider other)
    {
        if (itemDisplay.itemAmount[11] == 1)
        {
            bossRoomBlock.transform.Translate(Vector3.down * 2 * Time.deltaTime, Space.World);
        }
    }
}
