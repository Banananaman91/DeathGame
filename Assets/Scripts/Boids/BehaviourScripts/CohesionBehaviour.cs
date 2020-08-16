using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boids.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Boids/Behaviour/Cohesion")]
    public class CohesionBehaviour : BoidBehaviour
    {
        public override Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader)
        {
            var average = new Vector3(0, 0, 0);
        
            //If no neighbours, return no adjustment
            if (neighbours.Count == 0) return average;
        
            //Calculate the average position of neighbours
            average = neighbours.Aggregate(average, (current, neighbour) => current + neighbour.position);
        
            //Reduce the average down to the average between all neighbours, removing player position and reducing down to 1%
            average /= neighbours.Count;
            average = (average - boid.transform.position);
            return average;
        }
    }
}
