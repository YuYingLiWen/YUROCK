#if UNITY_EDITOR
using UnityEngine;

namespace YuSystem.Utilities
{
    public class SpawnZoneGizmos : MonoBehaviour
    {
        [SerializeField] private Transform[] points = null;

        public Transform[] Points => points;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawSphere(points[i].position, 1f);
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            Gizmos.DrawSphere(points[points.Length - 1].position, 1f);
        }
    }
}
#endif
