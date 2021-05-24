using UnityEngine;

namespace YuSystem.Gameplay.VFX
{
    public class UIText : Actor
    {
        private readonly Vector3 zClamp = new Vector3(0f, 0f, -10f);

        private void OnEnable()
        {
            Invoke(nameof(Disable), 2f);
        }

        private void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime * 0.5f + zClamp);
        }
    }
}
