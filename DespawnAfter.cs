
using UnityEngine;
using YuSystem.Gameplay.Components;

namespace YuSystem.Utilities
{
    public class DespawnAfter : MonoBehaviour, IDisable
    {
        [SerializeField] private float time = 1f;

        private void OnEnable()
        {
            Invoke(nameof(Disable), time);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}

