using System.Collections.Generic;
using UnityEngine;

namespace Boids
{
    public class BoidRules
    {
        /*
         *
         * boid rule 1
         * Boids try to fly towards centre of mass of neighbouring boids (cohesion)
         * new average vector
         * if there are no neighbours
         * return average
         * for each neighbour
         * avg += neighbour position
         * return average / neighbour count
         *
         *
         * boid rule 2
         * boids try to keep a small distance away from other objects (including other boids) (separation)
         * new separation vector
         * for each neighbour boid
         * distance = current position - neighbour position
         * if distance < separation distance
         * separation -= distance
         *
         * return seperation
         *
         *
         * boid rule 3
         * boids try to match the velocity of nearby boids
         * new vector velocity
         * for each neighbour
         * velocity += neighbour velocity
         * return velocity
         *
         * boid rule 4
         * boid tends towards common goal (leader)
         * pass in goal position and boid
         * return goal position - boid position / 100
         */

        public Vector3 BoidRule1(Boid boid, List<Rigidbody> neighbours) // cohesion
        {
            var average = new Vector3(0, 0, 0);
            var boidRb = boid.BoidRigidbody;

            if (neighbours.Count == 0) return average;

            foreach (var neighbour in neighbours)
            {
                average += neighbour.position;
            }

            // average /= neighbours.Count;
            // average -= boidRb.position;
            
            var n = 1;
            if (neighbours.Count > n) n = neighbours.Count - 1;
            average /= n;
            average = (average - boidRb.velocity) / 100;
            var direction = average;
            return direction;
        }

        public Vector3 BoidRule2(Boid boid, List<Rigidbody> neighbours) // separation
        {
            var boidRb = boid.BoidRigidbody;
            var separation = new Vector3(0, 0, 0);
            if (neighbours.Count == 0) return separation;

            foreach (var neighbour in neighbours)
            {
                var distanceVector = neighbour.position - boidRb.position;
                if (Vector3.Distance(neighbour.position, boidRb.position) < boid.NeighbourSeparationDistance) separation -= distanceVector;
            }

            return separation;
        }

        public Vector3 BoidRule3(Boid boid, List<Rigidbody> neighbours) // match velocity
        {
            var averageVelocity = new Vector3(0, 0, 0);
            var boidRb = boid.BoidRigidbody;
            if (neighbours.Count == 0) return averageVelocity;

            foreach (var neighbour in neighbours)
            {
                averageVelocity += neighbour.velocity;
            }

            var n = 1;
            if (neighbours.Count > n) n = neighbours.Count - 1;
            averageVelocity /= n;
            averageVelocity = (averageVelocity - boidRb.velocity) / 8;
            return averageVelocity;
        }
        
        public Vector3 BoidRule4(Boid boid) // tend towards leader
        {
            Vector3 direction;
            if (Vector3.Distance(boid.transform.position, boid.Leader.position) < boid.LeaderDistance)
            {
                direction = (boid.Leader.position + boid.BoidRigidbody.position) / 100;
                return direction;
            }
            direction = (boid.Leader.position - boid.BoidRigidbody.position) / 100;
            return direction;
        }

        public void BoidRule5(Boid boid, List<Rigidbody> enemies) // tend away from enemy
        {
            var separation = new Vector3(0, 0, 0);
            var boidRb = boid.BoidRigidbody;

            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(boidRb.position, enemy.position) > boid.NeighbourRange) continue;
                separation = enemy.position - boidRb.position;
            }
            boidRb.AddForce(-separation.normalized * (boid.MovementSpeed * Time.deltaTime), ForceMode.Impulse);
        }

        public Vector3 BoidRule6(Boid boid, List<Rigidbody> enemies) // separation from enemy
        {
            var boidRb = boid.BoidRigidbody;
            var separation = new Vector3(0, 0, 0);

            foreach (var enemy in enemies)
            {
                var distanceVector = boidRb.position - enemy.position;
                if (Vector3.Distance(boidRb.position, enemy.position) < boid.EnemySeparationDistance) separation -= distanceVector;
            }

            return -separation;
        }
    }
}
