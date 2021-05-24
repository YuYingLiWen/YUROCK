using System.Collections;
using UnityEngine;
using YuSystem.Gameplay.ScriptableObjects;
using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : RigidActor
    {
        [SerializeField] private SO_Laser script = null;

        [SerializeField] private SkinnedMeshRenderer meshRend = null;
        //TODO: Move these to SOs;
        public static Vector3 hitZoneP0, hitZoneP1; 
        public static Vector3 moveZoneP0, moveZoneP1; //The zone where the bat can move to.

        private LineRenderer lineR = null;
        private Animation fireAnim = null;

        private Vector3 hitZonePicked, moveDestination;
        private Renderer rend;

        private int health = 7;
        private bool isDrugged = false;

        private Coroutine flashCoroutine = null;

        protected override void Awake()
        {
            base.Awake();
            base.ForceTag(Tags.Enemy);
            base.ForceLayer(Layers.Enemies);

            lineR = GetComponent<LineRenderer>();
            fireAnim = GetComponent<Animation>();
            rend = GetComponent<Renderer>();

        }

        private void OnEnable()
        {
            if (Yu_GameManager.Instance.State != GameState.Play) return;

             Yu_GameManager.Instance.OnGameStop += Disable;

            StartLaser();

            lineR.enabled = false;
            fireAnim.Stop();
        }

        protected override void Start()
        {
            base.Start();

            lineR.positionCount = 2;

            hitZonePicked.z = 0f;
            hitZonePicked.y = hitZoneP0.y;

            moveDestination.z = 0f;
        }

        private void OnDisable()
        {
            if (Yu_GameManager.Instance.State != GameState.Play) return;

            if (Yu_GameManager.Instance) Yu_GameManager.Instance.OnGameStop -= Disable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"{Tags.Projectile}"))
            {
                if (isDrugged) return;

                health--;

                if (health < 1)
                {
                    fireAnim.Stop();
                    lineR.enabled = false;
                    isDrugged = true;
                    meshRend.material = script.YellowBatMat;
                    Invoke(nameof(StunnedCD), 5f);
                    Yu_StatsManager.Instance.AddScoreFlat(100);
                }
                else
                {
                    if (flashCoroutine != null)
                    {
                        StopCoroutine(flashCoroutine);
                    }

                    flashCoroutine = StartCoroutine(FlashRoutine());

                    IEnumerator FlashRoutine()
                    {
                        meshRend.material = script.WhiteBatMat;
                        yield return script.WaitForFlash;
                        meshRend.material = script.PurpleBatMat;
                        flashCoroutine = null;
                    }
                }
            }
        }

        private void StunnedCD()
        {
            isDrugged = false;
            health = 7;
            meshRend.material = script.PurpleBatMat;
            fireAnim.Play();
        }

        [ContextMenu("START LASER")]
        public void StartLaser()
        {
            PickMoveToDestination();
            MoveBatToDestination();
        }

        //Called by animation events
        private void Fire()
        {
            if (isDrugged) return;

            RaycastHit[] hits;

            Vector3 normDirection = (lineR.GetPosition(1) - lineR.GetPosition(0)).normalized;

            hits = Physics.RaycastAll(lineR.GetPosition(0), normDirection , script.MaxRaycastDistance, 1 << LayerMask.NameToLayer($"{Layers.Asteroids}"), QueryTriggerInteraction.Ignore);
            
            foreach (RaycastHit h in hits)
            {
                if (!h.collider) return;

                if (h.collider.TryGetComponent(out Asteroid asteroid))
                {
                    if (asteroid.IsExplosive) asteroid.Explode();
                    asteroid.Split();
                }
            }
            
            if (Physics.Raycast(lineR.GetPosition(0), normDirection, out RaycastHit hit, script.MaxRaycastDistance, 1 << LayerMask.NameToLayer($"{Layers.Player}"), QueryTriggerInteraction.Ignore))
            {
                GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.OnHitIndicator, hit.point);
                hit.collider.GetComponent<Player>().Health.TakeDamage(script.Damage);
            }
        }

        private void PickHitZone()
        {
            if (isDrugged) return;

            hitZonePicked.x = Random.Range(hitZoneP0.x, hitZoneP1.x);

            lineR.SetPosition(1, hitZonePicked);
        }

        [ContextMenu("Pick Destination")]
        private void PickMoveToDestination()
        {
            moveDestination.x = Random.Range(moveZoneP0.x, moveZoneP1.x);
            moveDestination.y = Random.Range(moveZoneP1.y, moveZoneP0.y);
            lineR.SetPosition(0, moveDestination);
        }

        private void MoveBatToDestination()
        {
            float timeElapsed = 0f;
            Vector3 start = transform.position; // Force fix a bug where bats will spawn at [0,0,0]
            Vector3 destination = lineR.GetPosition(0);

            StartCoroutine(MoveRoutine());

            IEnumerator MoveRoutine()
            {
                while ((timeElapsed / script.MoveTime) < 1f)
                {
                    transform.position = Vector3.Slerp(start, destination, timeElapsed / script.MoveTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                PickHitZone();
                fireAnim.Play();
            }
        }
    }
}
