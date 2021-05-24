using UnityEngine;

namespace YuSystem.Gameplay
{
    public class UITimer : Timer
    {
        [SerializeField] private UnityEngine.UI.Text cdRemainingText = null;
        [SerializeField] private UnityEngine.UI.Image targetGraphic = null;
        [SerializeField] private UnityEngine.UI.Image fillCircle = null;

        protected override void OnEnable()
        {
            base.OnEnable();

            OnCDTimeUpdate += UpdateCDText;
            OnCDStart += OnStart;
            OnCDTerminate += OnTerminate;

            fillCircle.fillAmount = 0f;
            cdRemainingText.text = System.String.Empty;
            targetGraphic.raycastTarget = true;
            fillCircle.raycastTarget = false;
            cdRemainingText.raycastTarget = false;
        }

        private void OnDisable()
        {
            OnCDTimeUpdate -= UpdateCDText;
            OnCDStart -= OnStart;
            OnCDTerminate -= OnTerminate;
        }

        private void UpdateCDText(float cdTime, float timeElapsed) 
        {
            cdRemainingText.text = $"{(cdTime - timeElapsed).ToString("F1")}";
            fillCircle.fillAmount = (cdTime - timeElapsed) / cdTime;
        }

        private void OnTerminate()
        {
            targetGraphic.raycastTarget = true;
            cdRemainingText.enabled = false;
            timeElapsed = 0f;
        }

        private void OnStart()
        {
            cdRemainingText.enabled = true;
            targetGraphic.raycastTarget = false;
        }
    }
}

