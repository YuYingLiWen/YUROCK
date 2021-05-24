using UnityEngine;

namespace YuSystem.Gameplay.Utilities
{
    [CreateAssetMenu(fileName = "New SceneBeautifier", menuName = "YU_SOs/Scene Beautifier")]
    public class SO_SceneBeautifier : ScriptableObject
    {
        [SerializeField] private GameObject[] treePrefabs = null;

        public int Length => treePrefabs.Length;

        /// <summary>
        /// Get a random tree.
        /// </summary>
        public GameObject GetTree() => treePrefabs[Random.Range(0, Length)];

        /// <summary>
        /// Get a specified tree.
        /// </summary>
        public GameObject GetTree(int index)
        {
            if (index > treePrefabs.Length)
            {
                Debug.LogError($"Index OOB. Index: {index} | Length {treePrefabs.Length}");
                return treePrefabs[0];
            }

            return treePrefabs[index];
        }
    }
}