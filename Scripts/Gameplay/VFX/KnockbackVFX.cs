using UnityEngine;
using YuSystem.Gameplay.Utilities;

namespace YuSystem.Gameplay.VFX
{
    public class KnockbackVFX : Actor
    {

        //Called by animation event
        private void Knockback(RigidActor rigid)
        {
            Vector3 explosionPos = transform.position;
            float explosionSize = transform.localScale.x * 6f;
            float explosionForce = 4f * explosionSize;

            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionSize, 1 << LayerMask.NameToLayer($"{Layers.Asteroids}"), QueryTriggerInteraction.Ignore);

            foreach (Collider c in colliders)
            {
                if (!c.attachedRigidbody) return;

                if (c.attachedRigidbody != rigid) c.attachedRigidbody.AddExplosionForce(explosionForce, explosionPos, explosionSize, 1f, ForceMode.Impulse);
            }
        }
    }
}
