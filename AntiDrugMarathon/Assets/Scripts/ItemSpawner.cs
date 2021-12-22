using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    public List<GameObject> items;
    public GameObject player;

    int rannumX;
    int rannumY;
    float posZ;

    public List<GameObject> itemPool;

    private void Start()
    {
        instance = this;
    }

    public void SpawnNewItem(int itemcount, int addcount)
    {
        posZ = 200/ itemcount;
        GameObject prefab = items[Random.Range(0, items.Count)];

        if (itemPool.Count>0)
        {
            prefab = itemPool[0];
            itemPool.RemoveAt(0);
            if(prefab.tag=="Drug")
            {
                prefab.GetComponent<DrugController>().Resettype();
            }
            prefab.transform.position = new Vector3(ranX(), ranY(), player.transform.position.z + (posZ * addcount) + 100);
            prefab.SetActive(true);
        }
        else
        {     
            Instantiate(prefab, new Vector3(ranX(), ranY(), player.transform.position.z+(posZ* addcount)+100), Quaternion.identity);
        }
    }

    public int ranX()
    {
        rannumX = Random.Range(1, 4);

        switch (rannumX)
        {
            case 1:
                return -3;
            case 2:
                return 0;
            case 3:
                return 3;
        }
        return 0;
    }

    public int ranY()
    {
        rannumY = Random.Range(0, 2);

        if (rannumY == 1)
        {
            return 2;
        }
        else
        {
            return 5;
        }
    }
    
}
