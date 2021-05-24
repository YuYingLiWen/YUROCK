using System.Collections;
using UnityEngine;
using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float waitTime = 0.3f;
        private WaitForSeconds waitForRof;

        private void Awake()
        {
            waitForRof = new WaitForSeconds(waitTime);
        }

        private void OnEnable()
        {
            if (Yu_GameManager.Instance.State != GameState.Play) return;
            CancelInvoke();
            Invoke(nameof(Fire), 1f);
        }

        public void Fire()
        {
            StopAllCoroutines();
            if (!gameObject.activeSelf) return;
            StartCoroutine(ShootRoutine());

            IEnumerator ShootRoutine()
            {
                while (true)
                {
                    GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.PlayerProjectile, transform.position, transform.rotation);
                    yield return waitForRof;
                }
            }
        }
    }
}

