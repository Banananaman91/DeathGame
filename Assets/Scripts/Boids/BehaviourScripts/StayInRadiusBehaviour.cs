using System.Collections.Generic;
using UnityEngine;

namespace Boids.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Boids/Behaviour/Stay In Radius")]
    public class StayInRadiusBehaviour : BoidBehaviour
    {
        [SerializeField] private Vector3 _centre;
        [SerializeField] private float _radius = 15f;
        
        public override Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader)
        {
            var centreOffset = _centre - boid.transform.position;
            var t = centreOffset.magnitude / _radius;
            if (t < 0.9f) return Vector3.zero;

            return centreOffset * (t * t);
        }
    }
}
