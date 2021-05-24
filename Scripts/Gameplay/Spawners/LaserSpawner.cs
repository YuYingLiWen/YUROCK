using UnityEngine;
using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class LaserSpawner : Spawner
    {
        [SerializeField] private Transform hitZoneP0 = null, hitZoneP1 = null;
        [SerializeField] private Transform moveZoneP0 = null, moveZoneP1 = null; //The zone where the bat can move to.

        private int difficultyIndex = 0;
        private bool initialized = false;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawHitZone();
            DrawMoveZone();

            void DrawHitZone()
            {
                Gizmos.DrawLine(hitZoneP0.position, hitZoneP1.position);
            }

            void DrawMoveZone()
            {
                Gizmos.DrawLine(moveZoneP0.position, new Vector3(moveZoneP1.position.x, moveZoneP0.position.y));
                Gizmos.DrawLine(moveZoneP0.position, new Vector3(moveZoneP0.position.x, moveZoneP1.position.y));
                Gizmos.DrawLine(moveZoneP1.position, new Vector3(moveZoneP1.position.x, moveZoneP0.position.y));
                Gizmos.DrawLine(moveZoneP1.position, new Vector3(moveZoneP0.position.x, moveZoneP1.position.y));
            }
        }
#endif

        private void OnEnable()
        {
            Yu_StatsManager.Instance.OnDifficultyIncrease += IncreaseDifficulty;
            Yu_GameManager.Instance.OnGamePrewarm += () => difficultyIndex = 0;
        }

        [ContextMenu("Spawn")]
        public override void Spawn()
        {
            if (!initialized)
            {
                Laser.hitZoneP0 = hitZoneP0.position;
                Laser.hitZoneP1 = hitZoneP1.position;
                Laser.moveZoneP0 = moveZoneP0.position;
                Laser.moveZoneP1 = moveZoneP1.position;
                initialized = true;
            }
            GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.Laser, transform.localPosition);
        }

        protected override void IncreaseDifficulty()
        {
            if(difficultyIndex++ % 3 == 0) Spawn();
        }
    }
}