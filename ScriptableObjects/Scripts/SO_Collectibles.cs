using UnityEngine;

namespace YuSystem.Gameplay.ScriptableObjects
{
    public abstract class SO_Collectibles: ScriptableObject
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private int score = 20;

        public float Speed { get => speed; }
        public int Score { get => score; }
    }
}