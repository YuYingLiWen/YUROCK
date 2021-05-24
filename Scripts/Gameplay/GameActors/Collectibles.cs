using UnityEngine;
using YuSystem.Gameplay.Utilities;

namespace YuSystem.Gameplay
{
    public abstract class Collectibles : RigidActor
    {
        protected override void Awake()
        {
            base.Awake();
            base.ForceTag(Tags.Collectibles);
            base.ForceLayer(Layers.Collectibles);

            gameObject.layer = LayerMask.NameToLayer($"{Layers.Collectibles}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"{Tags.AsteroidOOB}")) Disable(); // If OOB, using the AsteroidOOB instead.

            if (!other.CompareTag($"{Tags.Player}")) return;

            GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.PointsText, new Vector3(transform.position.x, transform.position.y, -10f));

            AddScore();
            Disable();
        }

        protected abstract void AddScore();

        protected abstract void Update();
    }
}