using UnityEngine;

namespace YuSystem.Managers
{
    public class MG_HelpPanel : MonoBehaviour
    {
        private int index = 0;

        private int Index
        {
            get => index;
            set
            {
                index = value > helpImages.Length - 1 ? helpImages.Length - 1 : value < 0 ? 0 : value;
                OnIndexChange?.Invoke();
            }
        }

        [SerializeField] private GameObject[] helpImages = null;

        private void OnEnable()
        {
            Index = 0;
        }

        private void Start()
        {
            OnIndexChange += PanelToggler;
        }

        public void Next() => Index++;

        public void Previous() => Index--;

        private void PanelToggler()
        {
            for (int i = 0; i < helpImages.Length; i++)
            {
                if (i == index) helpImages[i].SetActive(true);
                else helpImages[i].SetActive(false);
            }
        }

        private event System.Action OnIndexChange;
    }
}