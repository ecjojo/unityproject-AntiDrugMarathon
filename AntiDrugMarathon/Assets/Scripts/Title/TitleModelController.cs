using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleModelController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up, 45 * Time.deltaTime, Space.Self);

    }
}
