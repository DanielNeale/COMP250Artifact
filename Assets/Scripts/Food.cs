using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private int foodRemaining;
    [SerializeField]
    private GameObject foodParticle;


    public void GetFood(int amount, Transform ant)
    {
        foodRemaining -= amount;
        Instantiate(foodParticle, ant);

        if (foodRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }
}
