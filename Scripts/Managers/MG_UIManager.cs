using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YuSystem.Managers
{
    public class MG_UIManager : MonoBehaviour
    {
        [SerializeField] private Text personalBestScore = null;
        [SerializeField] private Text highScore = null;
        [SerializeField] private Text gameTimeElapsed = null;
        [SerializeField] private Text currScore = null;
        [SerializeField] private Button buttonStart = null;
        [SerializeField] private Button buttonExit = null;
        [SerializeField] private GameObject splashPanel = null;
        [SerializeField] private GameObject scorePanel = null;
        [SerializeField] private GameObject gameOverStatsPanel = null;

        private void OnEnable()
        {
            Yu_StatsManager.Instance.OnGameTimeElapsed += UpdateTimer;
            Yu_StatsManager.Instance.OnScoreUpdate += UpdateCurrentScore;
            if (Yu_GameManager.Instance)
            {
                Yu_GameManager.Instance.OnGameStart += ScorePanelToggler;
                Yu_GameManager.Instance.OnGameStop += EnableGameOverScoreBoard;
            }

            UpdateHighScore();
            UpdatePersonalBest();

            buttonStart.onClick.AddListener(PrewarmGame);
            buttonExit.onClick.AddListener(ExitGame);
        }

        private void Start()
        {
            splashPanel.SetActive(true);
            scorePanel.SetActive(false);
        }

        private void OnDisable()
        {
            if (Yu_GameManager.Instance)
            {
                Yu_GameManager.Instance.OnGameStart -= ScorePanelToggler;
                Yu_GameManager.Instance.OnGameStop -= SplashPanelToggler;
            }
        }

        private void PrewarmGame()
        {
            Yu_GameManager.Instance.State = GameState.Prewarm;

            splashPanel.SetActive(false);
        }

        //TODO: Hook to Scene Manager
        private void ExitGame()
        {
            StartCoroutine(LoadSceneRoutine());

            System.Collections.IEnumerator LoadSceneRoutine()
            {
                AsyncOperation async = SceneManager.LoadSceneAsync("MinigamesSceneLoaderScene");
                async.allowSceneActivation = true;
                yield return null;
            }

            //Application.Quit();
        }

        private void ScorePanelToggler()
        {
            scorePanel.SetActive(!scorePanel.activeSelf);
            if(scorePanel.activeSelf) ClearTable();
        }
        private void SplashPanelToggler() => splashPanel.SetActive(!splashPanel.activeSelf);

        public void EnableGameOverScoreBoard()
        {
            gameOverStatsPanel.SetActive(true);
            scorePanel.SetActive(false);
            splashPanel.SetActive(false);
        }

        private void ClearTable()
        {
            personalBestScore.text = "Personal Best: 999999";
            gameTimeElapsed.text = "Score: 00:00:00";

            UpdateHighScore();
            UpdatePersonalBest();
        }

        private void UpdateTimer(float value) => gameTimeElapsed.text = TimeSpan.FromSeconds(value).ToString("mm':'ss':'ff");

        private void UpdatePersonalBest()
        {
            //Network
            Debug.LogWarning("Not Implemented");
        }

        private void UpdateHighScore()
        {
            //Network
            Debug.LogWarning("Not Implemented");
            highScore.text = "Highscore: 999999";
        }

        private void UpdateCurrentScore(int score) => currScore.text = $"Score: {score.ToString().PadLeft(6, '0')}";

    }
}
