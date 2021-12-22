using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    RoadSpawner roadSpawner;
    ItemSpawner itemSpawner;

    public int itemcount;

    void Start()
    {
        instance = this;

        roadSpawner = GetComponent<RoadSpawner>();
        itemSpawner = GetComponent<ItemSpawner>();

        switch (LevelData.curLevel)
        {
            case 0:
                itemcount = 5;
                return;
            case 1:
                itemcount = 6;
                return;
            case 2:
                itemcount = 7;
                return;
        }
    }


    public void SpawnTrggerEntered()
    {
        roadSpawner.MoveRoad();

        for (int i = 0; i < itemcount; i++)
        {
            itemSpawner.SpawnNewItem(itemcount,i);
        }
    }
}
