using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    public float yOffset = 4f;
    public float zOffset = -10f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        
            transform.position = new Vector3(0, yOffset, player.position.z + zOffset);
        
    }
}
