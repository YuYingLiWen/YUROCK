using UnityEngine;
using UnityEngine.UI;

namespace YuSystem.Gameplay
{
    public class HelpTextBox : MonoBehaviour
    {
        private Text helpText = null;
        private Animation changeColor = null;

        private void Awake()
        {
            helpText = GetComponent<Text>();
            changeColor = GetComponent<Animation>();
        }


        public void OnPointerDown(string str)
        {
            changeColor.Stop();

            if (str == $"{HelpBox.Asteroid_Normal}") helpText.text = "Heavy Rock!";
            else if (str == $"{HelpBox.Asteroid_Boom}") helpText.text = "Boom!";
            else if(str == $"{HelpBox.ExtraPoints}") helpText.text = "More Points!";
            else if(str == $"{HelpBox.Laser}") helpText.text = "Evade Laser!";
        }

        public void OnPointerUp()
        {
            helpText.text = "YUROCK!";
            changeColor.Play();
        }
    }
}
