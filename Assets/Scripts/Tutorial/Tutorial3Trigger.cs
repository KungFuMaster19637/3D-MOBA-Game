using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3Trigger : MonoBehaviour
{
    public GameObject[] items;
    public Transform[] itemSpawnLocations;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private ItemDisplay tutorialItemDisplay;

    private bool spawnStarted;
    private List<GameObject> listOfItems = new List<GameObject>();

    private void Start()
    {
        spawnStarted = false;
    }
    private void Update()
    {
        if (spawnStarted)
        {
            CheckItemsPickedUp();
        }
    }

    public void SpawnItems()
    {
        inventoryUI.SetActive(true);
        for (int i = 0; i < items.Length; i++)
        {
            GameObject item = Instantiate(items[i], itemSpawnLocations[i]);
            listOfItems.Add(item);
            item.GetComponent<Item>().itemDisplay = tutorialItemDisplay; 
        }
        spawnStarted = true;
    }

    private void CheckItemsPickedUp()
    {
        for (int i = 0; i < listOfItems.Count; i++)
        {
            if (listOfItems[i].GetComponent<Item>().isPickedUp == false)
            {
                return;
            }
        }
        TutorialManager.Instance.Tutorial3();


    }

    [ContextMenu("Skip Tutorial")]
    public void SkipTutorial3()
    {
        TutorialManager.Instance.Tutorial3();
    }
}
