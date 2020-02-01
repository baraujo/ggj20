using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float degressPerSecond = 360f;
    
    void Update()
    {
        transform.Rotate(0, 0, -degressPerSecond * Time.deltaTime);
    }
}
