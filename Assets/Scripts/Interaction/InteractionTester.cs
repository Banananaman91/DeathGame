using System;
using MovementNEW;
using UnityEngine;

namespace Interaction
{
    public class InteractionTester : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;
        [SerializeField] private FurnitureInteract _fu;
        [SerializeField] private PlayerMovement _player;
        private bool _triggered;
        private float _count;

        private void Update()
        {
            if (Input.GetKey(_key) && !_triggered)
            {
                _fu.Interact(_player);
                _count = 0;
            }

            if (!_triggered) return;
            _count += Time.deltaTime;
            if (_count > 5) _triggered = false;
        }
    }
}
