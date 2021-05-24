using UnityEngine;
using UnityEngine.UI;

namespace YuSystem.Managers
{
    public class MG_EndGameScoreBoardManager : MonoBehaviour
    {
        [SerializeField] private Text powerUpsScore = null;
        [SerializeField] private Text destructionScore = null;
        [SerializeField] private Text survivalScore = null;

        [SerializeField] private Text destructionMultiplier = null;
        [SerializeField] private Text powerUpsMultiplier = null;
        [SerializeField] private Text survivalMultiplier = null;

        [SerializeField] private Text powerUpsTotal = null;
        [SerializeField] private Text destructionTotal = null;
        [SerializeField] private Text survivalTotal = null;

        [SerializeField] private Text grandTotalScore = null;

        private void OnEnable()
        {
            DisplayScores();
        }

        private void DisplayScores()
        {
            powerUpsScore.text = SpaceFormat(Yu_StatsManager.Instance.PowerUpsScore);
            destructionScore.text = SpaceFormat(Yu_StatsManager.Instance.DestructionScore);
            survivalScore.text = SpaceFormat(Yu_StatsManager.Instance.SurvivalScore);

            DisplayIndexDifficulty(ref destructionMultiplier);
            DisplayIndexDifficulty(ref powerUpsMultiplier);
            DisplayIndexDifficulty(ref survivalMultiplier);

            powerUpsTotal.text = SpaceFormat(Yu_StatsManager.Instance.PowerUpsTotalScore);
            destructionTotal.text = SpaceFormat(Yu_StatsManager.Instance.DestructionTotalScore);
            survivalTotal.text = SpaceFormat(Yu_StatsManager.Instance.SurvivalTimeTotalScore);

            grandTotalScore.text = SpaceFormat(Yu_StatsManager.Instance.GrandTotalScore);

            void DisplayIndexDifficulty(ref Text t) => t.text = "x " + Yu_GameManager.Instance.GlobalDifficultyIndex;

            string SpaceFormat(int num) => num.ToString().PadLeft(6, ' ');
        }
    }
}