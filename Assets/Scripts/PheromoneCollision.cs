using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneCollision : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) != transform && Vector3.Distance(transform.position, transform.parent.GetChild(i).position) < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
