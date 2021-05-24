using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace YuSystem.Gameplay
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float cdTime = 0f;
        protected float timeElapsed = 0f;

        private bool isRunning = false;

        protected virtual void OnEnable()
        {
            isRunning = false;
            timeElapsed = 0f;
        }

        public void StartCounting()
        {
            if (isRunning) return;

            isRunning = true;

            StartCoroutine(CounterRoutine());

            IEnumerator CounterRoutine()
            {
                OnCDStart?.Invoke();
                OnActivated?.Invoke();
                while(timeElapsed < cdTime)
                {
                    timeElapsed += Time.deltaTime;
                    OnCDTimeUpdate?.Invoke(cdTime, timeElapsed);
                    yield return null;
                }
                OnCDTerminate?.Invoke();
                isRunning = false;
            }
        }

        public event Action<float, float> OnCDTimeUpdate;
        public event Action OnCDStart;
        public event Action OnCDTerminate;

        public UnityEvent OnActivated;
    }
}
