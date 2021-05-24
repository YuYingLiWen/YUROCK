using System.Collections;
using UnityEngine;
using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class AsteroidSpawner : Spawner
    {
        private const float scaleMax = 5;
        private const float scaleMin = 2;

        private const float p0x = -20f;
        private const float p1x = 20f;
        private const float initWaitTime = 2.5f;
        [SerializeField] private float waitTimeBetweenSpawn = 2.5f;
        private const float difficultyRatio = 0.90f;
        private WaitForSecondsRealtime waitForSpawnDelay;

#if DEBUG
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(new Vector3(p0x, transform.position.y), 3f);
            Gizmos.DrawSphere(new Vector3(p1x, transform.position.y), 3f);
            Gizmos.DrawLine(new Vector3(p0x, transform.position.y), new Vector3(p1x, transform.position.y));
        }
#endif

        protected void Awake()
        {
            waitForSpawnDelay = new WaitForSecondsRealtime(waitTimeBetweenSpawn);
        }

        private void OnEnable()
        {
            Yu_GameManager.Instance.OnGamePrewarm += Stop;
            Yu_GameManager.Instance.OnGamePrewarm += ResetValues;
            Yu_GameManager.Instance.OnGameStart += Spawn;
            Yu_GameManager.Instance.OnPlayerDeath += Stop;
            Yu_StatsManager.Instance.OnDifficultyIncrease += IncreaseDifficulty;
        }

        private void OnDisable()
        {
            Yu_GameManager.Instance.OnGamePrewarm -= Stop;
            Yu_GameManager.Instance.OnGamePrewarm -= ResetValues;
            Yu_GameManager.Instance.OnGameStart -= Spawn;
            Yu_GameManager.Instance.OnGameStop -= Stop;
            Yu_StatsManager.Instance.OnDifficultyIncrease -= IncreaseDifficulty;
        }

        public override void Spawn()
        {
            float randScale;
            Vector3 nextPos;

            nextPos.y = transform.position.y;
            nextPos.z = 0f;

            StartCoroutine(SpawnMeteoritesRoutine());

            IEnumerator SpawnMeteoritesRoutine()
            {
                while (true)
                {
                    randScale = Random.Range(scaleMin, scaleMax);
                    nextPos.x = Random.Range(p0x, p1x);

                    if (Random.Range(0, 100) < 5f) GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.ExtraPoints, nextPos);
                    else GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.Asteroid, nextPos, Vector3.one * randScale);
                    yield return waitForSpawnDelay;
                }
            }
        }

        private void ResetValues()
        {
            SpawnRate(initWaitTime);
        }

        protected override void IncreaseDifficulty()
        {
            waitTimeBetweenSpawn *= difficultyRatio;
            SpawnRate(waitTimeBetweenSpawn);
        }

        /// <param name="delay">In seconds.</param>
        public void SpawnRate(in float delay)
        {
            waitTimeBetweenSpawn = delay;
            waitForSpawnDelay = new WaitForSecondsRealtime(delay);
        }

        /// <summary> Stops spawning. </summary>
        public void Stop() => StopAllCoroutines();
    }
}
