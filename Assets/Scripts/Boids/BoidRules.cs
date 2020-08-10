using System.Collections.Generic;
using System.Linq;
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

        public Vector3 BoidCohesion(Boid boid, List<Rigidbody> neighbours) // cohesion
        {
            var average = new Vector3(0, 0, 0);
            var boidRb = boid.BoidRigidbody;

            if (neighbours.Count == 0) return average;

            average = neighbours.Aggregate(average, (current, neighbour) => current + neighbour.position);
            var n = 1;
            if (neighbours.Count > n) n = neighbours.Count;
            average /= n;
            average = (average - boidRb.position) / 100;
            return average;
        }

        public Vector3 BoidSeparation(Boid boid, List<Rigidbody> neighbours) // separation
        {
            var boidRb = boid.BoidRigidbody;
            var separation = new Vector3(0, 0, 0);
            return neighbours.Count == 0 ? separation : (from neighbour in neighbours let distanceVector = neighbour.position - boidRb.position where Vector3.Distance(neighbour.position, boidRb.position) < boid.NeighbourSeparationDistance select distanceVector).Aggregate(separation, (current, distanceVector) => current - distanceVector);
        }

        public Vector3 BoidVelocity(Boid boid, List<Rigidbody> neighbours) // match velocity
        {
            var averageVelocity = new Vector3(0, 0, 0);
            var boidRb = boid.BoidRigidbody;
            if (neighbours.Count == 0) return averageVelocity;

            averageVelocity = neighbours.Aggregate(averageVelocity, (current, neighbour) => current + neighbour.velocity);

            var n = 1;
            if (neighbours.Count > n) n = neighbours.Count;
            averageVelocity /= n;
            averageVelocity = (averageVelocity - boidRb.velocity) / 8;
            return averageVelocity;
        }
        
        public Vector3 BoidLeader(Boid boid) // tend towards leader
        {
            Vector3 direction;
            if (Vector3.Distance(boid.transform.position, boid.Leader.position) < boid.LeaderDistance) return Vector3.zero;
            direction = (boid.Leader.position - boid.BoidRigidbody.position) / 100;
            return direction;
        }

        public Vector3 BoidEnemy(Boid boid, List<Rigidbody> enemies) // separation from enemy
        {
            var boidRb = boid.BoidRigidbody;
            var separation = new Vector3(0, 0, 0);

            separation = (from enemy in enemies let distanceVector = boidRb.position - enemy.position where Vector3.Distance(boidRb.position, enemy.position) < boid.EnemySeparationDistance select distanceVector).Aggregate(separation, (current, distanceVector) => current - distanceVector);

            return -separation;
        }
    }
}
