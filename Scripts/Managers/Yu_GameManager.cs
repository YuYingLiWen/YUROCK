using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YuSystem.Managers
{
    public class Yu_GameManager : MonoBehaviour
    {
        private static Yu_GameManager instance = null;

        public static Yu_GameManager Instance
        {
            get
            {
                if (!instance) instance = FindObjectOfType<Yu_GameManager>();
                return instance;
            }
        }

        private GameState state = GameState.Stopped;
        public int GlobalDifficultyIndex { get; set; } = 1;

        const float fixedDelta = 0.02f;

        private readonly WaitForFixedUpdate waitForFixUpdate = new WaitForFixedUpdate();

        public GameState State
        {
            get => state;
            set
            {
                state = value;
                if (state == GameState.Prewarm) OnGamePrewarm?.Invoke();
                else if (state == GameState.Play) OnGameStart?.Invoke();
                else if (state == GameState.Stopped) OnGameStop?.Invoke();
            }
        }

        private void Awake()
        {
            CreateInstance();
        }

        private void OnDestroy()
        {
            instance = null;
            OnGamePrewarm = null;
            OnGameStart = null;
            OnGameStop = null;
            OnPlayerDeath = null;
        }

        public void GameOver()
        {
            if (state != GameState.Play) return;

            StartSlowMotion();
        }

        public void StartSlowMotion()
        {
            StartCoroutine(SlowMotion_Routine());

            IEnumerator SlowMotion_Routine()
            {
                float timeElapsed = 0;
                float lerpDuration = 0.5f;
                const float b = 0.05f;

                while (Time.timeScale > b)
                {
                    Time.timeScale = Mathf.Lerp(1f, b, timeElapsed / lerpDuration);
                    Time.fixedDeltaTime = Mathf.Clamp(fixedDelta * Time.timeScale, 0f, fixedDelta);
                    timeElapsed += Time.unscaledDeltaTime;
                    yield return waitForFixUpdate;
                }
                StopSlowMotion();
            }
        }

        private void StopSlowMotion()
        {
            float timeElapsed = 0;
            float lerpDuration = 3f;

            StartCoroutine(StopSlowMotion_Routine());

            IEnumerator StopSlowMotion_Routine()
            {
                while (Time.timeScale < 1f)
                {
                    Time.timeScale = Mathf.Lerp(0.05f, 1f, timeElapsed / lerpDuration);
                    Time.fixedDeltaTime = Mathf.Clamp(fixedDelta * Time.timeScale, 0f, fixedDelta);
                    timeElapsed += Time.unscaledDeltaTime;
                    yield return waitForFixUpdate;
                }
            }

            State = GameState.Stopped;
            OnPlayerDeath?.Invoke();
        }

        private void CreateInstance()
        {
            if (Instance == null) instance = this;
            else if (Instance != this) Destroy(Instance.gameObject);
        }

        public void ToggleShadow(Image image)
        {
            Light l = FindObjectOfType<Light>();

            if (l.shadows == LightShadows.Hard)
            {
                l.shadows = LightShadows.None;
                image.color = Color.clear;
            }
            else
            {
                l.shadows = LightShadows.Hard;
                image.color = Color.black;
            }
        }

        /// <summary>
        /// Called before OnGameStart.
        /// </summary>
        public event Action OnGamePrewarm;

        /// <summary>
        /// Called after OnGamePrewarm.
        /// </summary>
        public event Action OnGameStart;

        /// <summary>
        /// Called when player dies & before OnGameStop.
        /// </summary>
        public event Action OnPlayerDeath;

        /// <summary>
        /// Called when game has ended & after OnGameStop.
        /// </summary>
        public event Action OnGameStop;
    }
}
