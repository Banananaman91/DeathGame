using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boids.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Boids/Behaviour/Steered Cohesion")]
    public class SteeredCohesionBehaviour : BoidBehaviour
    {
        private Vector3 _currentVelocity;
        [SerializeField] private float _smoothTime = 0.5f;
        public override Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader)
        {
            var average = new Vector3(0, 0, 0);
        
            //If no neighbours, return no adjustment
            if (neighbours.Count == 0) return average;
        
            //Calculate the average position of neighbours
            average = neighbours.Aggregate(average, (current, neighbour) => current + neighbour.position);
        
            //Reduce the average down to the average between all neighbours, removing player position and reducing down to 1%
            average /= neighbours.Count;
            var transform = boid.transform;
            average = (average - transform.position);
            average = Vector3.SmoothDamp(transform.forward, average, ref _currentVelocity, _smoothTime);
            return average;
        }
    }
}
