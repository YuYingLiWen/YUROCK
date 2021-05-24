using UnityEngine;
using YuSystem.Gameplay.Utilities;

namespace YuSystem.Gameplay
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public abstract class RigidActor : Actor
    {
        protected Rigidbody rigid;
        protected Collider coll;

        protected virtual void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
        }

        protected void ForceTag(Tags tag) => gameObject.tag = $"{tag}";
        protected void ForceLayer(Layers layer) => gameObject.layer = LayerMask.NameToLayer($"{layer}");
    }
}