using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroybytime : MonoBehaviour
{
    public float lifetime = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
