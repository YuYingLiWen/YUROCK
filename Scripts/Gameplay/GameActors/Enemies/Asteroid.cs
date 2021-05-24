using UnityEngine;
using YuSystem.Gameplay.Components;
using YuSystem.Gameplay.ScriptableObjects;
using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class Asteroid : RigidActor, ISpawnable
    {
        [SerializeField] private SO_Asteroid script = null;
        public bool IsExplosive { get; private set; } = false;
        private MeshRenderer meshR;
        private Renderer rend;

        protected override void Awake()
        {
            base.Awake();
            base.ForceTag(Tags.Enemy);
            base.ForceLayer(Layers.Asteroids);

            meshR = GetComponent<MeshRenderer>();
            rend = GetComponent<Renderer>();
        }

        protected void OnEnable()
        {
            if (Yu_GameManager.Instance.State != GameState.Play) return;

            IsExplosive = Random.Range(0, script.UpperChanceBound) < script.ExplosiveChance;

            if (IsExplosive)
            {
                meshR.material.color = script.RedTone;

                rigid.mass = transform.localScale.x * script.ExplosiveMassRatio;
            }
            else
            {
                meshR.material.color = Color.white;

                if (transform.localScale.x < script.MinScale)
                {
                    rigid.mass = script.MinScale * 0.5f;
                    rigid.AddRelativeTorque(Vector3.one * Random.Range(script.RockMinTorque, script.RockMaxTorque));
                }
                else
                {
                    rigid.mass = transform.localScale.x * script.NormalMassRatio;
                    rigid.AddRelativeTorque(Vector3.one * Random.Range(script.AsteroidMinTorque, script.AsteroidMaxTorque));
                }
            }

            rigid.velocity = Vector3.zero;
            rigid.AddForce((Vector3.right * Random.Range(-1f, 1f)).normalized * script.SplitForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!rend.isVisible) return;

            else if ((collision.collider.CompareTag($"{Tags.Floor}") 
                || collision.collider.CompareTag($"{Tags.Player}")) && IsExplosive)
            {
                Explode();
                Destroy();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!rend.isVisible) return;

            if (other.CompareTag($"{Tags.Projectile}"))
            {
                if (IsExplosive) Explode();
                Split();
                Yu_StatsManager.Instance.AddScoreDestruction(SO_Asteroid.Score);
            }
            else if (other.CompareTag($"{Tags.AsteroidOOB}")) FakeDisable();
        }

        public void Split()
        {
            if (!rend.isVisible) return;
            Spawn();
            Destroy();
        }

        public void Spawn()
        {
            float scale = transform.localScale.x * 0.5f;

            if (scale < script.MinScale)
            {
                Destroy();
                return;
            }

            for (int i = 0; i < Random.Range(script.MinSplitNum, script.MaxSplitNum); i++)
            {
                GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.Asteroid, transform.position, Vector3.one * scale);
            }
        }

        public void Destroy()
        {
            GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.DestructionVFX, transform.position);
            Disable();
        }

        public void Explode()
        {
            GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.ExplosionVFX, transform.position);

            Vector3 explosionPos = transform.position;
            float explosionSize = transform.localScale.x * script.ExplodeSizeMulti;
            float explosionForce = script.ExplodeForceMulti * explosionSize;

            Rigidbody rigid;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionSize, ~(1 << LayerMask.GetMask($"{Layers.Player}", $"{Layers.Asteroids}")));

            foreach (Collider c in colliders)
            {
                rigid = c.attachedRigidbody;
                if (rigid)
                {
                    rigid.AddExplosionForce(explosionForce, explosionPos, explosionSize);
                    rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, script.MaxVelocity);
                }
            }
        }

        private void FakeDisable()
        {
            Invoke(nameof(this.Disable), 3f);
        }
    }
}

