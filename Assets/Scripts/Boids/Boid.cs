using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boids
{
    [RequireComponent(typeof(Rigidbody), (typeof(SphereCollider)))]
    public class Boid : MonoBehaviour
    {
        [SerializeField] private int _neighbourSeparationDistance;
        [SerializeField] private int _enemySeparationDistance;
        [SerializeField] private float _leaderDistance;
        [SerializeField] private SphereCollider _sphere;
        [SerializeField] private int _neighbourRange;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private List<Rigidbody> _neighboursRigidbodies = new List<Rigidbody>();
        [SerializeField] private List<Rigidbody> _enemyRigidbodies = new List<Rigidbody>();
        private Rigidbody _leader;
        private BoidRules _boidRules = new BoidRules();
        public int NeighbourSeparationDistance => _neighbourSeparationDistance;
        public int EnemySeparationDistance => _enemySeparationDistance;
        //public float LeaderDistance => _leaderDistance;
        public Rigidbody BoidRigidbody => GetComponent<Rigidbody>();
        public List<Rigidbody> NeighboursRigidbodies => _neighboursRigidbodies;
        public float MovementSpeed => _movementSpeed;
        public Rigidbody Leader => _leader;
        
        public int NeighbourRange => _neighbourRange;
        public int LeaderDistance { get; set; }
        private void Awake()
        {
            if (Math.Abs(_sphere.radius - _neighbourRange) > float.Epsilon) _sphere.radius = _neighbourRange;
            if (!_sphere.isTrigger) _sphere.isTrigger = true;
        }

        private void FixedUpdate()
        {
            var direction = new Vector3(0, 0, 0);

            direction += _boidRules.BoidRule1(this, _neighboursRigidbodies);
            direction += _boidRules.BoidRule2(this, _neighboursRigidbodies);
            direction += _boidRules.BoidRule3(this, _neighboursRigidbodies);
            direction += _boidRules.BoidRule4(this);
            if (_enemyRigidbodies.Count > 0) direction += _boidRules.BoidRule6(this, _enemyRigidbodies);

            BoidRigidbody.velocity = Vector3.ClampMagnitude(BoidRigidbody.velocity, MovementSpeed);
            BoidRigidbody.AddForce(direction.normalized * (MovementSpeed * Time.deltaTime), ForceMode.Impulse);
            //BoidRigidbody.AddForce(transform.forward * (_movementSpeed * Time.deltaTime), ForceMode.Impulse);
        }

        public void AddNeighbour(Rigidbody neighbour)
        {
            _neighboursRigidbodies.Add(neighbour);
        }

        public void AddLeader(Rigidbody leader)
        {
            _leader = leader;
        }

        private void OnTriggerEnter(Collider other)
        {
            var isBoid = other.GetComponent<Boid>();
            var rbObject = other.GetComponent<Rigidbody>();
            if (_neighboursRigidbodies.Contains(rbObject)) return;
            if (isBoid && rbObject) _neighboursRigidbodies.Add(rbObject);
            if (isBoid || !rbObject) return;
            if (_enemyRigidbodies.Contains(rbObject) || rbObject == _leader) return;
            _enemyRigidbodies.Add(rbObject);
        }

        private void OnTriggerExit(Collider other)
        {
            var isBoid = other.GetComponent<Boid>();
            var rbObject = other.GetComponent<Rigidbody>();
            if (!_neighboursRigidbodies.Contains(rbObject)) return;
            if (isBoid && rbObject) _neighboursRigidbodies.Remove(rbObject);
            if (isBoid || !rbObject) return;
            if (!_enemyRigidbodies.Contains(rbObject)) return;
            _enemyRigidbodies.Remove(rbObject);
        }
    }
}
