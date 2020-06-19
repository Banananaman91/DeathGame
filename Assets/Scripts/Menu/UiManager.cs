using System;
using UnityEngine;

namespace Menu
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _controls;
        [SerializeField] private GameObject _inventory;

        private void Update()
        {
            if (_inventory.activeSelf)
            {
                foreach (var control in _controls)
                {
                    control.SetActive(false);
                }
            }
            else
            {
                foreach (var control in _controls)
                {
                    control.SetActive(true);
                }
            }
        }
    }
}
