using System;
using UnityEngine;
using System.Collections;

namespace YuSystem.Managers
{
    public class Yu_StatsManager : MonoBehaviour
    {
        private static Yu_StatsManager instance;

        public static Yu_StatsManager Instance
        {
            get
            {
                if (!instance) instance = FindObjectOfType<Yu_StatsManager>();
                return instance;
            }
        }

        private float startTime;
        public float GameTime { get; private set; }
        private float gameTimeElapsed;
        private int difficultyIndex = 0;
        private const int perPoints = 100;

        private int currentScore;
        private int collectiblesScore;
        private int destructionScore;

        public delegate void AddScoreDelegate(int value);

        public int CurrentScore
        {
            get { return currentScore; }
        }

        public int PowerUpsScore
        {
            get { return collectiblesScore; }
        }

        public int DestructionScore
        {
            get { return destructionScore; }
        }

        /// <summary>
        /// This function increase the difficulty of the game.
        /// </summary>
        public void AddScoreDestruction(int value)
        {
            destructionScore += ClampInt(ref value);
            currentScore += value;

            OnScoreUpdate?.Invoke(currentScore);
            if ((currentScore / perPoints) > difficultyIndex)
            {
                difficultyIndex++;
                OnDifficultyIncrease?.Invoke();
            }
        }

        /// <summary>
        /// This function is used for when PowerUps are collected.
        /// </summary>
        public void AddScorePowerUps(int value)
        {
            collectiblesScore += ClampInt(ref value);
            currentScore += value;
            OnScoreUpdate?.Invoke(currentScore);
        }

        /// <summary>
        /// This function is used when you would like to simply add a score.
        /// </summary>
        public void AddScoreFlat(int value)
        {
            currentScore += ClampInt(ref value);
            OnScoreUpdate?.Invoke(currentScore);
        }

        public int SurvivalScore
        {
            get { return (int)gameTimeElapsed * 10; }
        }

        public float TimeElapsed
        {
            get { return gameTimeElapsed; }
            set
            {
                gameTimeElapsed = value < 0 ? 0 : value > float.MaxValue ? float.MaxValue : value;
                OnGameTimeElapsed?.Invoke(gameTimeElapsed);
            }
        }

        private void Awake()
        {
            CreateInstance();
        }

        private void OnEnable()
        {
            Yu_GameManager.Instance.OnGamePrewarm += RestValues;

            Yu_GameManager.Instance.OnGameStart += SetStartTime;
            Yu_GameManager.Instance.OnGameStart += StartTimeCounter;

            Yu_GameManager.Instance.OnGameStop += CalculateGameTime;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private int FinalScore()
        {
            int elapsedScore = (int)gameTimeElapsed * 5;

            return elapsedScore + currentScore;
        }

        private void RestValues()
        {
            difficultyIndex = 0;
            currentScore = 0;
            gameTimeElapsed = 0;
            collectiblesScore = 0;
            destructionScore = 0;
        }

        public int SurvivalTimeTotalScore => CalculateTotalScore(SurvivalScore);
        public int DestructionTotalScore => CalculateTotalScore(DestructionScore);
        public int PowerUpsTotalScore => CalculateTotalScore(PowerUpsScore);
        public int GrandTotalScore => SurvivalTimeTotalScore + DestructionTotalScore + PowerUpsTotalScore;

        public void StartTimeCounter()
        {
            StartCoroutine(TimeCounterRoutine());

            IEnumerator TimeCounterRoutine()
            {
                while (true)
                {
                    TimeElapsed += Time.deltaTime;
                    yield return null;
                }
            }
        }

        private int CalculateTotalScore(int score) => score * Yu_GameManager.Instance.GlobalDifficultyIndex;

        public int FindHighScore()
        {
            int networkNumber = 0;
            return Mathf.Max(GrandTotalScore, networkNumber);
        }

        private void CreateInstance()
        {
            if (!Instance) instance = this;
            else if (Instance != this) Destroy(Instance.gameObject);
        }

        private int ClampInt(ref int num) => num = num < 0 ? 0 : num > int.MaxValue ? int.MaxValue : num;

        private void SetStartTime() => startTime = Time.time;

        private void CalculateGameTime() => GameTime = Time.time - startTime;

        public event Action<int> OnScoreUpdate;

        public event Action<float> OnGameTimeElapsed;

        public event Action OnDifficultyIncrease;
    }
}