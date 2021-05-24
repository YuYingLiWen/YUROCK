using UnityEngine;
using YuSystem.Gameplay.Utilities;

namespace YuSystem.Gameplay
{
    public class Projectile : RigidActor
    {
        private const float speed = 35f;

        protected override void Awake()
        {
            base.Awake();
            base.ForceTag(Tags.Projectile);
            base.ForceLayer(Layers.Projectiles);
        }

        protected override void Start()
        {
            base.Start();

            coll.isTrigger = true;
            rigid.isKinematic = true;
        }

        protected void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"{Tags.Enemy}")
            || other.CompareTag($"{Tags.AsteroidOOB}")) Disable();
        }

        private void OnBecameInvisible()
        {
            Disable();
        }

        public void Move()
        {
            rigid.MovePosition(rigid.position + transform.up * speed * Time.deltaTime);
        }
    }
}

