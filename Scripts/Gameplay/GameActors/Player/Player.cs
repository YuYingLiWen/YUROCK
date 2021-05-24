using System;
using UnityEngine;
using YuSystem.Gameplay.Components;
using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class Player : RigidActor
    {
        private const float jetPackForce = 20f;
        private const float moveSpeedMultiplier = 5f;
        [SerializeField] private int hp = 1;

        public Health Health { get; private set; }
        [SerializeField] private GameObject knockbackVFX = null;
        private JetPack jet = null;
        [SerializeField] private Gun gun = null;
        [SerializeField] private Animator animator = null;

        private bool isGrounded = false;

        private readonly int idleHash = Animator.StringToHash("Idle");
        private readonly int runHash = Animator.StringToHash("Run");

        protected override void Awake()
        {
            base.Awake();
            base.ForceTag(Tags.Player);
            base.ForceLayer(Layers.Player);

            jet = GetComponent<JetPack>();

            Health = new Health(hp);
            Health.OnDeath += Die;
        }



        protected void OnEnable()
        {
            if (Yu_GameManager.Instance.State != GameState.Play)
            {
                Disable();
                return;
            }

            Respawn();
        }

        private void FixedUpdate()
        {
            if (!Health.IsAlive ) return;

            if (Input.touchCount <= 0)
            {
                PlayAnim(idleHash);

                //Reduce Speed
                if (isGrounded && Mathf.Abs(rigid.velocity.x) > 0f) rigid.AddForce(-Vector3.right * rigid.velocity.x * 13f, ForceMode.Force);

                return;
            }
            else
            {
                if(isGrounded) PlayAnim(runHash);
                Move();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"{Tags.PlayerOOB}")) DieFallOff();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag($"{Tags.Floor}")) isGrounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag($"{Tags.Floor}")) isGrounded = false;
        }

        private void OnDisable()
        {
            if (Yu_GameManager.Instance.State != GameState.Play) return;

            OnKilled -= CameraFollow.Instance.OnPlayerDeath;
            OnKilled -= Yu_GameManager.Instance.GameOver;
            OnKilled -= Yu_StatsManager.Instance.StopAllCoroutines;

            OnFallOff -= CameraFollow.Instance.OnPlayerDeath;
            OnFallOff -= Yu_StatsManager.Instance.StopAllCoroutines;
        }

        private void Move()
        {
            float direction = InGamePointer.Instance.transform.position.x - transform.position.x;
            const float temp = 2f;

            if(Mathf.Abs(rigid.velocity.x) < 2f)
            {
                if (direction > 0f) rigid.AddForce(Vector3.right * temp, ForceMode.VelocityChange);
                else if (direction < 0f) rigid.AddForce(-Vector3.right * temp, ForceMode.VelocityChange);
            }

            rigid.AddForce(Mathf.Clamp(direction, -5f, 5f) * moveSpeedMultiplier, 0, 0, ForceMode.Acceleration);
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, 8f);
        }

        //Called by event action
        public void Knockback() => knockbackVFX.SetActive(true);

        //Called by button event
        public void UseJetPack()
        {
            jet.Use(true);

            rigid.velocity = Vector3.right * rigid.velocity.x;
            rigid.AddForce(transform.up * jetPackForce, ForceMode.Impulse);
        }

        private void Die()
        {
            OnKilled?.Invoke();
            Disable();
        }

        private void DieFallOff()
        {
            OnFallOff?.Invoke();
            Disable();
        }

        private void Respawn()
        {
            OnRespawn?.Invoke();

            Health.Reset();

            rigid.velocity = Vector3.zero;

            gun.Fire();

            CameraFollow.Instance.OnPlayerRespawn(transform);

            OnKilled += CameraFollow.Instance.OnPlayerDeath;
            OnKilled += Yu_GameManager.Instance.GameOver;
            OnKilled += Yu_StatsManager.Instance.StopAllCoroutines;


            OnFallOff += CameraFollow.Instance.OnPlayerDeath;
            OnFallOff += Yu_StatsManager.Instance.StopAllCoroutines;
        }

        private void PlayAnim(int hash) => animator.Play(hash);

        public event Action OnRespawn;
        public event Action OnKilled;
        public event Action OnFallOff;
    }
}
