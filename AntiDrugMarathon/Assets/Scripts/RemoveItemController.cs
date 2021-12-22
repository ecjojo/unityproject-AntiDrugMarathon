using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("RemoveItemTrigger");

        if (other.tag == "Love")
        {
            ItemSpawner.instance.itemPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Drug")
        {
            ItemSpawner.instance.itemPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
