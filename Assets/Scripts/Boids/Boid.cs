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
        [SerializeField] private SphereCollider _sphere;
        [SerializeField] private int _neighbourRange;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _maxLeaderDist;
        [SerializeField] private List<Rigidbody> _neighboursRigidbodies = new List<Rigidbody>();
        [SerializeField] private List<Rigidbody> _enemyRigidbodies = new List<Rigidbody>();
        private int _forwardDistance = 5;
        private Rigidbody _leader;
        private BoidRules _boidRules = new BoidRules();
        public int NeighbourSeparationDistance => _neighbourSeparationDistance;
        public int EnemySeparationDistance => _enemySeparationDistance;
        public Rigidbody BoidRigidbody => GetComponent<Rigidbody>();
        public Transform RbTransform => BoidRigidbody.transform;
        public float MovementSpeed => _movementSpeed;
        public Rigidbody Leader => _leader;
        public List<Rigidbody> NeighboursRigidbodies => _neighboursRigidbodies;
        public int NeighbourRange => _neighbourRange;
        public int LeaderDistance { get; set; }
        public float Max => _maxLeaderDist;
        public float Min => -_maxLeaderDist;
        private void Awake()
        {
            if (Math.Abs(_sphere.radius - _neighbourRange) > float.Epsilon) _sphere.radius = _neighbourRange;
            if (!_sphere.isTrigger) _sphere.isTrigger = true;
        }

        private void FixedUpdate()
        {
            var direction = new Vector3(0, 0, 0);
            //direction += RbTransform.forward * _forwardDistance;
            direction += _boidRules.BoidCohesion(this, _neighboursRigidbodies);
            direction += _boidRules.BoidSeparation(this, _neighboursRigidbodies);
            direction += _boidRules.BoidVelocity(this, _neighboursRigidbodies);
            direction += _boidRules.BoidLeader(this);
            if (_enemyRigidbodies.Count > 0) direction += _boidRules.BoidEnemy(this, _enemyRigidbodies);
            transform.Rotate(direction * (_turnSpeed * Time.deltaTime), Space.Self);
            BoidRigidbody.velocity = Vector3.ClampMagnitude(BoidRigidbody.velocity, MovementSpeed);
            var directionVector = ((RbTransform.right * direction.x) + (RbTransform.up * direction.y) + (RbTransform.forward * direction.z)) * _forwardDistance;
            BoidRigidbody.MovePosition(BoidRigidbody.transform.position + directionVector * (_movementSpeed * Time.deltaTime));
        }
        public void AddNeighbour(Rigidbody neighbour)
        {
            _neighboursRigidbodies.Add(neighbour);
        }
        public void AddLeader(Rigidbody leader)
        {
            _leader = leader;
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     var isBoid = other.GetComponent<Boid>();
        //     var rbObject = other.GetComponent<Rigidbody>();
        //     if (_neighboursRigidbodies.Contains(rbObject)) return;
        //     if (isBoid && rbObject) _neighboursRigidbodies.Add(rbObject);
        //     if (isBoid || !rbObject) return;
        //     if (_enemyRigidbodies.Contains(rbObject) || rbObject == _leader) return;
        //     _enemyRigidbodies.Add(rbObject);
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     var isBoid = other.GetComponent<Boid>();
        //     var rbObject = other.GetComponent<Rigidbody>();
        //     if (!_neighboursRigidbodies.Contains(rbObject)) return;
        //     if (isBoid && rbObject) _neighboursRigidbodies.Remove(rbObject);
        //     if (isBoid || !rbObject) return;
        //     if (!_enemyRigidbodies.Contains(rbObject)) return;
        //     _enemyRigidbodies.Remove(rbObject);
        // }
    }
}
