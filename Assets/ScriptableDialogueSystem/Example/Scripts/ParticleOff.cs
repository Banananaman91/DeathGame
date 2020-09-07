using UnityEngine;

namespace ScriptableDialogueSystem.Example.Scripts
{
    public class ParticleOff : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particle;

        private void Awake()
        {
            foreach (var particle in _particle)
            {
                particle.Stop();
            }
        }
    }
}
