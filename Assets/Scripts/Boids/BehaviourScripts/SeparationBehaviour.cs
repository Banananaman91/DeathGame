using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boids.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Boids/Behaviour/Separation")]
    public class SeparationBehaviour : BoidBehaviour
    {
        public override Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader)
        {
            var separation = new Vector3(0, 0, 0);
            var nAvoid = 0;

            foreach (var neighbour in neighbours.Where(neighbour => Vector3.SqrMagnitude(neighbour.position - boid.transform.position) < leader.SquareAvoidanceRadius))
            {
                nAvoid++;
                separation += boid.transform.position - neighbour.position;
            }

            if (nAvoid > 0) separation /= nAvoid;
            return separation;
        }
    }
}
