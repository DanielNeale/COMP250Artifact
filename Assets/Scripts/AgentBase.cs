using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBase : MonoBehaviour
{
    //Variables for movement
    [SerializeField]
    private float moveForce = .5f;
    [SerializeField]
    private float maxSpeed = 3;
    private Rigidbody rb;
    protected Vector3 moveTarget;

    //Variables for creating pheromones
    protected bool createTrail = false;
    [SerializeField]
    private GameObject pheremone = null;
    private Transform newPheremone;
    [SerializeField]
    private float pheremonePlaceTime = 1;
    private float pheremoneTimer;

    //Variables for detecting and following trails
    protected Transform currentPheromone;
    protected Vector3 home;

    //Variables for hunger
    [SerializeField]
    private float maxHunger;
    [SerializeField]
    private float starveSpeed;
    [SerializeField]
    private float hunger;
    protected bool eat;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTarget = transform.position;
        home = transform.position;
        hunger = maxHunger;
    }


    void FixedUpdate()
    {
        // Checks to see if a new target should be calculated
        if (Vector3.Distance(transform.position, moveTarget) < 0.2f || moveTarget == Vector3.zero)
        {
            moveTarget = CalcMoveTarget();
        }

        // Moves the agent
        if (moveTarget != Vector3.zero)
        {
            transform.LookAt(moveTarget);

            rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        // Creates pheremone trails
        if (createTrail)
        {
            if (pheremoneTimer < 0)
            {
                Instantiate(newPheremone.GetChild(0), transform.position, transform.rotation, newPheremone);
                pheremoneTimer = pheremonePlaceTime;
            }

            pheremoneTimer -= Time.deltaTime;
        }

        // Kills the agent from starvation
        if (hunger < 0)
        {
            Destroy(gameObject);
        }

        // Instructs agent to eat
        else if (hunger < maxHunger * .5f)
        {
            eat = true;
        }

        else
        {
            eat = false;
        }

        // Handles eating
        if (eat && transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            hunger = maxHunger;
        }

        hunger -= starveSpeed * Time.fixedDeltaTime;
    }


    /// <summary>
    /// Calculates where the agent should move to
    /// </summary>
    /// <returns> The position the agent should move to </returns>
    protected virtual Vector3 CalcMoveTarget()
    {
        // Basic movement keeps the agent around the hive
        Vector3 target = new Vector3(home.x + Random.Range(-5, 5), home.y, home.z + Random.Range(-5, 5));

        if (Vector3.Distance(target, transform.position) < 14)
        {
            return target;
        }

        return Vector3.zero;
    }


    void OnTriggerEnter(Collider other)
    {
        // Triggers when a food source is found
        if (other.transform.GetComponent<Food>())
        {
            FoundFood(other.transform);
        }

        // Triggers when a pheremone is found
        else if (other.transform.parent != null && other.transform.parent.GetComponent<Pheremone>())
        {
            currentPheromone = other.transform;
        }

        // Triggers when the agent is hungry and finds food
        if (other.CompareTag("food") && eat && transform.childCount == 0)
        {
            other.transform.parent = transform;
        }
    }


    /// <summary>
    /// Handles logic for when a food source is found
    /// </summary>
    /// <param name="food"> The food source </param>
    protected virtual void FoundFood(Transform food)
    {
        // Basic behaiviour to pick up a piece of food
        if (transform.childCount == 0)
        {
            food.GetComponent<Food>().GetFood(1, transform);
        }        
    }


    /// <summary>
    /// Triggers the start of a pheremone trail
    /// </summary>
    protected void StartTrail()
    {
        newPheremone = Instantiate(pheremone, transform.position, transform.rotation, null).transform;
        createTrail = true;
    }
}
