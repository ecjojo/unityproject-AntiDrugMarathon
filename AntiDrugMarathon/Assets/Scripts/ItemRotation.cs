using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    
    void Update()
    {
        this.transform.Rotate(Vector3.up, 45 * Time.deltaTime, Space.Self);
    }
}
