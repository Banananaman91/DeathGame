using System;
using UnityEngine;

namespace CandleLight
{
    public class CandleBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _candle;
        [SerializeField] private int _candleLimit;
        [SerializeField] private float _cooldownTime;
        [HideInInspector] public int CandleCount;
        private float _cooldown;
        private bool _coolingDown;

        private void Start()
        {
            CandleCount = 0;
        }

        private void Update()
        {
            if (_coolingDown)
            {
                _cooldown -= Time.deltaTime;
                if (_cooldown < 0) _coolingDown = false;
                return;
            }
            if (!Input.GetKey(KeyCode.T) || CandleCount >= _candleLimit) return;
            _coolingDown = true;
            _cooldown = _cooldownTime;
            var candle = Instantiate(_candle);
            candle.transform.position = transform.position;
            CandleCount++;
            var candleType = _candle.GetComponent<Candle>();
            candleType.CandleBehaviourObject = this;
        }
    }
}
