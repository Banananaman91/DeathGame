using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Boids
{
    public class BoidLeader : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [Header("Swarm")]
        [SerializeField] private GameObject _boid;
        [SerializeField, Range(1, 50)] private int _flockTotal;
        [SerializeField] private int _spawnRadius;
        [SerializeField] private int _leaderDistance;
        private List<Boid> _boidSwarm = new List<Boid>();

        private void Awake()
        {
            var transform1 = transform;
            var position = transform1.position;
            for (var i = 0; i < _flockTotal; i++)
            {
                var clone = Instantiate(_boid);
                clone.transform.position = position + Random.insideUnitSphere * _spawnRadius;
                clone.transform.forward = position - Random.insideUnitSphere * _spawnRadius;
                _boidSwarm.Add(clone.GetComponent<Boid>());
            }
            foreach (var boid in _boidSwarm)
            {
                foreach (var t in _boidSwarm.Where(t => !boid.NeighboursRigidbodies.Contains(t.BoidRigidbody) && t != boid))
                {
                    boid.AddNeighbour(t.BoidRigidbody);
                }
                boid.AddLeader(_rb);
                boid.LeaderDistance = _leaderDistance;
                boid.AddNeighbour(_rb);
            }
        }
    }
}
