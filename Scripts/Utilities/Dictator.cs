
using UnityEngine;
using YuSystem.Managers;
using UnityEngine.UI;
using YuSystem.Gameplay;

namespace YuSystem.Utilities
{
    public class Dictator : MonoBehaviour
    {
        [SerializeField] private Font font = null;

        private void Awake()
        {
            Application.targetFrameRate = -1;

            Screen.orientation = ScreenOrientation.Portrait;

            ForceFont();

#if UNITY_EDITOR
            RunDupeChecker();
#endif
        }

        private void ForceFont()
        {
            Text[] texts = Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[];

            foreach (Text text in texts)
            {
                text.font = font;
                if (text.fontSize < 20) text.fontSize = 20;
            }
        }

        private void OnDisable()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

#if UNITY_EDITOR
        public static void RunDupeChecker()
        {
            SanityCheck<MG_EndGameScoreBoardManager>();
            SanityCheck<Yu_GameManager>();
            SanityCheck<Yu_StatsManager>();
            SanityCheck<MG_UIManager>();
            SanityCheck<CameraFollow>();
        }

        private static void SanityCheck<T>() where T : Component
        {
            Component[] comps = Resources.FindObjectsOfTypeAll<T>();


            //Dupe Check
            if (comps.Length > 1) foreach (Component c in comps) Debug.LogError($"The Component [{c.GetType().Name}] is duped.", c.gameObject);

            else if( comps.Length < 1) Debug.LogError($"The Component [{typeof(T).Name}] is missing in the scene.");
        }
#endif
    }
}
