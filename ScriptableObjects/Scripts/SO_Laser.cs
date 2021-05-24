using UnityEngine;

namespace YuSystem.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Laser", menuName = "YU_SOs/Laser")]
    public class SO_Laser : ScriptableObject
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float maxRaycastDistance = 250f;
        [SerializeField] private float moveTime = 7f;

        [SerializeField] private Material purpleBatMat = null;
        [SerializeField] private Material yellowBatMat = null;
        [SerializeField] private Material whiteBatMat = null;

        public readonly WaitForSeconds WaitForFlash = new WaitForSeconds(0.1f);
        public int Damage { get => damage; }
        public float MaxRaycastDistance { get => maxRaycastDistance; }
        public float MoveTime { get => moveTime; }
        public Material PurpleBatMat { get => purpleBatMat; }
        public Material YellowBatMat { get => yellowBatMat; }
        public Material WhiteBatMat { get => whiteBatMat; }
    }
}