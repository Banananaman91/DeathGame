using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Boids
{
    [RequireComponent(typeof(Rigidbody), (typeof(SphereCollider)))]
    public class Boid : MonoBehaviour
    {
        [SerializeField] private Collider _boidCollider;
        public Collider BoidCollider => _boidCollider;
        private Transform BoidTransform => transform;

        public void MoveBoid(Vector3 velocity)
        {
            BoidTransform.forward = velocity;
            BoidTransform.position += velocity * Time.deltaTime;
        }
    }
}
