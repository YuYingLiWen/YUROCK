using UnityEngine;
using UnityEngine.UI;

namespace YuSystem.Gameplay
{
    public class GunManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] gunSets = null;
        [SerializeField] private Sprite[] sprites = null;
        [SerializeField] private Image img = null;

        private uint index = 0;

        private void OnEnable()
        {
            index = 0;

            SetGuns();
        }

        //Called by Button
        public void Toggle()
        {
            if (index + 1 >= gunSets.Length) index = 0;
            else index++;

            SetGuns();
        }

        private void SetGuns()
        {
            for (int i = 0; i < gunSets.Length; i++)
            {
                if (i == index)
                {
                    gunSets[i].SetActive(true);
                    img.sprite = sprites[i];
                }
                else gunSets[i].SetActive(false);
            }
        }
    }
}

