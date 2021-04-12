using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBase : MonoBehaviour
{
    [SerializeField]
    private float moveForce = .5f;
    [SerializeField]
    private float maxSpeed = 3;
    private Rigidbody rb;
    private Vector3 moveTarget;

    protected bool createTrail = false;
    [SerializeField]
    private GameObject pheremone = null;
    private Transform newPheremone;
    [SerializeField]
    private float pheremonePlaceTime = 1;
    private float pheremoneTimer;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTarget = transform.position;
    }


    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, moveTarget) < 0.2f)
        {
            moveTarget = CalcMoveTarget();
        }

        if (!(moveTarget == Vector3.zero))
        {
            transform.LookAt(moveTarget);

            rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        if (createTrail)
        {
            if (pheremoneTimer < 0)
            {
                Instantiate(newPheremone.GetChild(0), transform.position, transform.rotation, newPheremone);
                pheremoneTimer = pheremonePlaceTime;
            }

            pheremoneTimer -= Time.deltaTime;
        }
    }


    protected virtual Vector3 CalcMoveTarget()
    {
        //implement default movement

        return Vector3.zero;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            FoundFood();
        }
    }


    protected virtual void FoundFood()
    {
        //implement gathering food
    }


    protected void StartTrail()
    {
        newPheremone = Instantiate(pheremone, transform.position, transform.rotation, null).transform;
        createTrail = true;
    }
}
