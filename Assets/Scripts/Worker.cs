using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : AgentBase
{
    private bool gatherFood = true;


    /// <summary>
    /// Movement system for the worker
    /// Workers will stay in the hive until a pheremone signal is detected
    /// They will then follow the trail, collect food and bring it back to the hive
    /// </summary>
    /// <returns> The position the agent should move to </returns>
    protected override Vector3 CalcMoveTarget()
    {
        Vector3 target = Vector3.zero;

        // Move when there is a trail
        if (currentPheromone != null)
        {
            int trailPos = currentPheromone.GetSiblingIndex();
            Transform trail = currentPheromone.parent;

            // Move towards food
            if (gatherFood && trailPos != 0)
            {
                target = trail.GetChild(trailPos - 1).position;
            }

            // Move towards hive
            else if (!gatherFood && trailPos < trail.childCount - 1)
            {
                target = trail.GetChild(trailPos + 1).position;
            }

            // Reset behaiviour
            else
            {
                if (transform.childCount > 0)
                {
                    transform.GetChild(0).parent = null;
                }

                currentPheromone = null;
                gatherFood = true;
            }
        }

        // Stay in the hive without a trail
        else
        {
            return base.CalcMoveTarget();
        }

        return target;
    }


    // Triggers that food has been found
    protected override void FoundFood(Transform food)
    {
        gatherFood = false;
        base.FoundFood(food);
    }
}
