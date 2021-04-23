using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : AgentBase
{
    [SerializeField]
    private float moveVar = 0.1f;
    [SerializeField]
    private float targetlength = 1.5f;
    public bool returnBack = false;
    private Stack<Vector3> returnNodes = new Stack<Vector3>();


    private void Update()
    {
        //Updates whether it needs to go eat
        if (eat)
        {
            returnBack = true;
        }
    }


    /// <summary>
    /// Movement system for the scout
    /// Scouts will move randomly until they find a food source
    /// They will then create a pheremone trail back to the hive
    /// </summary>
    /// <returns> The position the agent should move to </returns>
    protected override Vector3 CalcMoveTarget()
    {
        Vector3 target = Vector3.zero;

        // Return back to the hive
        if (returnBack && returnNodes.Count > 0)
        {
            target = returnNodes.Pop();
        }

        // Find food to eat
        else if (eat)
        {
            createTrail = false;
            return base.CalcMoveTarget();
        }

        // Go out scouting
        else
        {
            returnBack = false;
            createTrail = false;
            target = new Vector3(Random.Range(-moveVar, moveVar), 0, 1) * targetlength;
            target = transform.TransformPoint(target);

            returnNodes.Push(target);
        }
        
        return target;
    }


    // Create a pheremone trail back to the hive
    protected override void FoundFood(Transform food)
    {
        returnBack = true;
        moveTarget = CalcMoveTarget();

        StartTrail();
    }
}
