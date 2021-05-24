
using UnityEngine;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class CameraFollow : MonoBehaviour
    {
        private static CameraFollow instance = null;

        public static CameraFollow Instance
        {
            get
            {
                if (!instance) instance = FindObjectOfType<CameraFollow>();
                return instance;
            }
        }

        private Transform player;
        [SerializeField] private Transform defaultLookAt = null;

        private Vector3 camPos;

        private const float zOffset = -30f;
        private const float yOffset = 12f;

        [SerializeField] private float speed = 1f;

        private bool isPaused = false;

        private void Start()
        {
            camPos.z = zOffset;
        }

        private void LateUpdate()
        {
            if (isPaused) return;
            if (!player)
            {
                if ((transform.position - camPos).magnitude < 1f) return;

                camPos.x = defaultLookAt.position.x;
                camPos.y = defaultLookAt.position.y + yOffset;
            }
            else if (player)
            {
                camPos.x = player.position.x;
                camPos.y = player.position.y + yOffset;
            }
            else return;

            transform.position = Vector3.Slerp(transform.position, camPos, Time.unscaledDeltaTime * speed);
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public void OnPlayerDeath()
        {
            player = null;
            camPos.x = defaultLookAt.position.x;
            camPos.y = defaultLookAt.position.y;

            isPaused = true;
            Invoke(nameof(DisablePause), 2f);
        }

        public void OnPlayerRespawn(Transform player)
        {
            this.player = player;
        }

        private void DisablePause()
        {
            isPaused = false;
            Yu_GameManager.Instance.GameOver();
        }
    }
}
