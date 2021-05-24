
using UnityEngine;

namespace YuSystem.Utilities
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float rotX = 0f;
        [SerializeField] private float rotY = 0f;
        [SerializeField] private float rotZ = 0f;

        void Update()
        {
            transform.Rotate(rotX, rotY, rotZ, Space.Self);
        }
    }
}
