using UnityEngine;
using UnityEngine.Events;

namespace CandleLight
{
    public class Candle : MonoBehaviour
    {
        [SerializeField] private float _lifetime;
        [SerializeField] private UnityEvent _behaviours;
        [HideInInspector] public CandleBehaviour CandleBehaviourObject;

        private void Start()
        {
            _behaviours.Invoke();
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime > 0) return;
            CandleBehaviourObject.CandleCount--;
            Destroy(gameObject);
        }
    }
}
