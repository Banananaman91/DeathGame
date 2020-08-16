using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Boids
{
    public class BoidLeader : MonoBehaviour
    {
        [Header("Swarm")]
        [SerializeField] private Boid _boid;
        [SerializeField, Range(1, 500)] private int _flockTotal = 250;
        [SerializeField, Range(0f, 2f)] private float _boidDensity = 0.08f;
        [SerializeField] private BoidBehaviour _behaviour;
        [SerializeField, Range(1f, 100f)] private float _driveFactor = 0.08f;
        [SerializeField, Range(1f, 100f)] private float _maxSpeed = 5f;
        [SerializeField, Range(1f, 10f)] private float _neighbourRadius = 1.5f;
        [SerializeField, Range(0f, 1f)] private float _avoidanceRadiusMultiplier = 0.5f;

        private float _squareMaxSpeed;
        private float _squareNeighbourRadius;
        private float _squareAvoidanceRadius;

        public float SquareAvoidanceRadius => _squareAvoidanceRadius;

        private List<Boid> _boidSwarm = new List<Boid>(); 
        
        
        private void Awake()
        {
            _squareMaxSpeed = _maxSpeed * _maxSpeed;
            _squareNeighbourRadius = _neighbourRadius * _neighbourRadius;
            _squareAvoidanceRadius = _squareNeighbourRadius * _avoidanceRadiusMultiplier * _avoidanceRadiusMultiplier;

            for (var i = 0; i < _flockTotal; i++)
            {
                Boid newBoid = Instantiate(
                    _boid,
                    Random.insideUnitSphere * (_flockTotal * _boidDensity),
                    Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                    transform
                    );
                _boidSwarm.Add(newBoid);
            }
        }

        private void FixedUpdate()
        {
            foreach (var agent in _boidSwarm)
            {
                var neighbours = GetNearbyObjects(agent);
                var move = _behaviour.CalculateMove(agent, neighbours, this);
                move *= _driveFactor;
                if (move.sqrMagnitude > _squareMaxSpeed) move = move.normalized * _maxSpeed;
                agent.MoveBoid(move);
            }
        }

        private List<Transform> GetNearbyObjects(Boid agent)
        {
            var neighbourColliders = Physics.OverlapSphere(agent.transform.position, _neighbourRadius);

            return (from c in neighbourColliders where c != agent.BoidCollider select c.transform).ToList();
        }
    }
}
