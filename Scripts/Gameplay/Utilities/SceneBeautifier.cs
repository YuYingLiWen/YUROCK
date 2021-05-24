using UnityEngine;
using YuSystem.Gameplay.Components;

namespace YuSystem.Gameplay.Utilities
{
    public class SceneBeautifier : MonoBehaviour, ISpawnable
    {
        [SerializeField] private SO_SceneBeautifier SO_SceneBeautifier = null;

        private GameObject obj;

        private void OnEnable() => Spawn();

        public void Spawn()
        {
            obj = Instantiate(SO_SceneBeautifier.GetTree(), transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x - 90f, Random.Range(0f, 180f), transform.rotation.z)); // TEST
            obj.transform.parent = transform;
        }
    }
}
