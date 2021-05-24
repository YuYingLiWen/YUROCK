using UnityEngine;
using YuSystem.Managers;

namespace YuSystem.Gameplay.Utilities
{
    public class TransitionController : MonoBehaviour
    {
        private Animation anim;

        private void Awake()
        {
            anim = GetComponent<Animation>();
        }

        private void OnEnable()
        {
            Yu_GameManager.Instance.OnGamePrewarm += Play;
        }

        private void OnDisable()
        {
            if (!Yu_GameManager.Instance) return;

            Yu_GameManager.Instance.OnGamePrewarm -= Play;
        }

        //Called by Annimation event
        private void ClearScene()
        {
            foreach (Actor act in FindObjectsOfType<Actor>()) act.Disable();
        }

        public void Play() => anim.Play();

        //Called by Annimation event
        private void StartGame()=> Yu_GameManager.Instance.State = GameState.Play;
    }
}
