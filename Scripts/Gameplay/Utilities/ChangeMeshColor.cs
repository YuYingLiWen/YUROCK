
using UnityEngine;

namespace YuSystem.Utilities
{
    public class ChangeMeshColor : MonoBehaviour
    {
        [SerializeField] private Color color = Color.magenta;

        private void Start()
        {
            GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
