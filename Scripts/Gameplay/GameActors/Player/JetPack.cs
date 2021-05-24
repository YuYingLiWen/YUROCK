using UnityEngine;

namespace YuSystem.Gameplay
{
    public class JetPack : MonoBehaviour
    {
        [SerializeField] private TrailRenderer[] smokeSystems = null;

        private void OnEnable()
        {
            Use(false);
        }

        public void Use(bool emit)
        {
            for (int i = 0; i < smokeSystems.Length; i++) smokeSystems[i].emitting = emit;

            Invoke(nameof(StopEmitting), 2f);
        }

        private void StopEmitting() => Use(false);
    }
}
