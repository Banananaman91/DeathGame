using System.Collections.Generic;
using UnityEngine;

namespace Boids.BehaviourScripts
{
    [CreateAssetMenu(menuName = "Boids/Behaviour/Composite")]
    public class CompositeBehaviour : BoidBehaviour
    {
        public BoidBehaviour[] _behaviours;
        public float[] _weights;

        public BoidBehaviour[] Behaviours
        {
            get => _behaviours;
            set => _behaviours = value;
        }

        public float[] Weights
        {
            get => _weights;
            set => _weights = value;
        }

        public override Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader)
        {
            //Handle data mismatch
            if (_weights.Length != _behaviours.Length)
            {
                Debug.LogError("Data mismatch in " + name + this);
                return Vector3.zero;
            }
            
            //Set up move
            var move = Vector3.zero;
            
            //Iterate through behaviours
            for (var i = 0; i < _behaviours.Length; i++)
            {
                var partialMove = _behaviours[i].CalculateMove(boid, neighbours, leader) * _weights[i];
                if (partialMove == Vector3.zero) continue;
                if (partialMove.sqrMagnitude > _weights[i] * _weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= _weights[i];
                }

                move += partialMove;
            }

            return move;
        }
    }
}
