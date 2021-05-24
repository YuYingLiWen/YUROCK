
using YuSystem.Gameplay.Utilities;

namespace YuSystem.Gameplay.VFX
{
    public class FireVFX: Actor
    {
        public void Detach()
        {
            transform.parent = GameObjectsPooler.Instance.transform;
            Invoke(nameof(base.Disable), 1f);
        }
    }
}
