using UnityEngine;

namespace YuSystem.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PowerUp", menuName = "YU_SOs/PowerUp")]
    public class SO_PowerUp : SO_Collectibles
    {
        [SerializeField] private float rotSpeed = 200f;

        public float RotSpeed { get => rotSpeed; }


        public int SpinRotation() => Random.Range(0, 2) == 0 ? -1 : 1;
    }
}
