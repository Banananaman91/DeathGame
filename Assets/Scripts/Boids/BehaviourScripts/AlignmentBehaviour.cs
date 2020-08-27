using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boids.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Boids/Behaviour/Alignment")]
    public class AlignmentBehaviour : BoidBehaviour
    {
        public override Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader)
        {
            var velocity = new Vector3(0, 0, 0);
        
            //If no neighbours, return no adjustment
            if (neighbours.Count == 0) return boid.transform.forward;
        
            //Calculate the average position of neighbours
            velocity = neighbours.Aggregate(velocity, (current, neighbour) => current + neighbour.transform.forward);
        
            //Reduce the average down to the average between all neighbours, removing player position and reducing down to 1%
            velocity /= neighbours.Count;
            return velocity;
        }
    }
}
