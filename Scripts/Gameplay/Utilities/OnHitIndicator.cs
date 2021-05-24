using UnityEngine;

namespace YuSystem.Gameplay.Utilities
{
    public class OnHitIndicator : MonoBehaviour
    {
        private void OnEnable()
        {
            Invoke(nameof(Disable), 3f);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}