using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    private Vector3 moveTarget;
    [SerializeField]
    private float moveForce = .5f;
    [SerializeField]
    private float maxSpeed = 3;
    [SerializeField]
    private float moveVar = 0.1f;
    [SerializeField]
    private float targetlength = 1.5f;
    private Rigidbody rb;

    public bool returnBack = false;
    private Stack<Vector3> returnNodes = new Stack<Vector3>();
    [SerializeField]
    private float nodePlaceTime = 1;
    private float nodeTimer;

    [SerializeField]
    private float antenneaLength = 2.0f;

    [SerializeField]
    private GameObject pheremone = null;
    private Transform newPheremone;
    private bool placePheremone = false;
    [SerializeField]
    private float pheremonePlaceTime = 1;
    private float pheremoneTimer;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTarget = transform.TransformPoint(new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)));
    }


    void Update()
    {
        // Find new target to move to
        if (Vector3.Distance(transform.position, moveTarget) < 0.2f)
        {
            CalculateTarget();
        }


        // Creates a stack of return nodes
        if (nodeTimer < 0 && !returnBack)
        {
            returnNodes.Push(transform.position);
            nodeTimer = nodePlaceTime;
        }

        else
        {
            nodeTimer -= Time.deltaTime;
        }


        if (placePheremone)
        {
            if (pheremoneTimer < 0)
            {
                Instantiate(newPheremone.GetChild(0), transform.position, transform.rotation, newPheremone);
                pheremoneTimer = pheremonePlaceTime;
            }

            pheremoneTimer -= Time.deltaTime;
        }

        if (!returnBack)
        {
            placePheremone = false;
        }


        // Detects objects infront of the boid
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * antenneaLength);

        if (Physics.Raycast(transform.position, transform.forward, out hit, antenneaLength))
        {
            if (hit.transform.CompareTag("Food"))
            {
                returnBack = true;
                CalculateTarget();
                CreateTrail();
            }
        }
    }

    
    void FixedUpdate()
    {
        moveTarget.y = transform.position.y;
        transform.LookAt(moveTarget);
        rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }


    private void CalculateTarget()
    {
        if (returnBack && returnNodes.Count > 0)
        {
            moveTarget = returnNodes.Pop();
        }

        else
        {
            returnBack = false;
            moveTarget = new Vector3(Random.Range(-moveVar, moveVar), 0, 1) * targetlength;
            moveTarget = transform.TransformPoint(moveTarget);
            Debug.DrawLine(transform.position, moveTarget, Color.red, 100);
        }
    }


    private void CreateTrail()
    {
        newPheremone = Instantiate(pheremone, transform.position, transform.rotation, null).transform;
        placePheremone = true;
    }
}
