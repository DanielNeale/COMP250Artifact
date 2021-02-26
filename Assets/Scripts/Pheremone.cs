using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheremone : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount > 1)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                Debug.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position, Color.blue);
            }
        }       
    }
}
