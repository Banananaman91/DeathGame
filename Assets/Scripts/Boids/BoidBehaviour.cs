using System.Collections;
using System.Collections.Generic;
using Boids;
using UnityEngine;

public abstract class BoidBehaviour : ScriptableObject
{
    public abstract Vector3 CalculateMove(Boid boid, List<Transform> neighbours, BoidLeader leader);
}
