using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int inventorySize = 4;
    [SerializeField]
    private GameObject[] inventorySlots;
    [SerializeField]
    private bool[] isEmpty;
    [SerializeField]
    private string[] slotObjectsPath;

    void Start()
    {
        inventorySlots = GameObject.FindGameObjectsWithTag("Inventory Slot");
        inventorySize = inventorySlots.Length;
        
        isEmpty = new bool[inventorySize];
        for (int i = 0; i < inventorySize; i++) isEmpty[i] = true;
        slotObjectsPath = new string[inventorySize];
        for (int i = 0; i < inventorySize; i++) slotObjectsPath[i] = "";
    }
    public bool getItem(GameObject newObject, string path)
    {
        if (newObject != null)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (isEmpty[i])
                {
                    slotObjectsPath[i] = path;
                    isEmpty[i] = false;
                    inventorySlots[i].GetComponent<Image>().enabled = true;
                    inventorySlots[i].GetComponent<Image>().sprite = newObject.GetComponent<SpriteRenderer>().sprite;
                    return true;
                }
            }
        }
        return false;
    }
    public void dropSlot(int numSlot)
    {
        if (isEmpty[numSlot] == false)
        {
            isEmpty[numSlot] = true;
            Instantiate(Resources.Load<GameObject>(slotObjectsPath[numSlot]), gameObject.transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, 0), Quaternion.identity);
            slotObjectsPath[numSlot] = "";
            inventorySlots[numSlot].GetComponent<Image>().sprite = null;
            inventorySlots[numSlot].GetComponent<Image>().enabled = false;
        }
    }
}
