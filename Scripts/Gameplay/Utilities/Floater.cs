using UnityEngine;

namespace YuSystem.Gameplay.Utilities
{
    public class Floater : MonoBehaviour
    {
        private float frequency;
        private float amplitude;
        private Vector3 temp;

        private Renderer rend = null;

        private void Awake()
        {
            frequency = Random.Range(1f, 1.5f);
            amplitude = Random.Range(1f, 2f);

            rend = GetComponentInChildren<Renderer>();
        }

        private void Start()
        {
            temp.x = transform.position.x;
            temp.z = transform.position.z;
        }

        private void Update()
        {
            if (!rend.isVisible) return;

            temp.y = transform.position.y + Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;

            transform.position = temp;
        }
    }
}
