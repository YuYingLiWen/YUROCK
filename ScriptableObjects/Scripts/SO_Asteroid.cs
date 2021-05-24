using UnityEngine;

namespace YuSystem.Gameplay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Asteroid", menuName = "YU_SOs/Asteroid")]
    public class SO_Asteroid : ScriptableObject
    {
        public const int Score = 5;

        [SerializeField] private float splitForce = 1;
        [SerializeField] private float minScale = 1f;
        [SerializeField] private float explosiveMassRatio = 0.65f;
        [SerializeField] private float normalMassRatio = 8f;
        [SerializeField] private float maxVelocity = 10f;
        [SerializeField] private Color redTone = Color.white; // 0.0588f; //15f / 255f
        [SerializeField] private int explosiveChance = 3;
        [Tooltip("Number is excluded.")]
        [SerializeField] private int upperChanceBound = 9;

        [Header("Torque")]
        [SerializeField] private int rockMinTorque = 50;
        [SerializeField] private int rockMaxTorque = 100;
        [SerializeField] private int asteroidMinTorque = 750;
        [SerializeField] private int asteroidMaxTorque = 1000;

        [Header("Explosion")]
        [SerializeField] private float explodeSizeMulti = 9f;
        [SerializeField] private float explodeForceMulti = 30f;

        [Header("Splits")]
        [SerializeField] private int minSplitNum = 3;
        [SerializeField] private int maxSplitNum = 5;


        public float SplitForce { get => splitForce; }
        public float MinScale { get => minScale; }
        public float ExplosiveMassRatio { get => explosiveMassRatio; }
        public float NormalMassRatio { get => normalMassRatio; }
        public float MaxVelocity { get => maxVelocity; }
        public Color RedTone { get=>redTone; }
        public int ExplosiveChance { get => explosiveChance; }
        public int UpperChanceBound { get => upperChanceBound; }
        public int RockMinTorque { get => rockMinTorque;  }
        public int RockMaxTorque { get => rockMaxTorque; }
        public int AsteroidMinTorque { get => asteroidMinTorque; }
        public int AsteroidMaxTorque { get => asteroidMaxTorque; }
        public float ExplodeSizeMulti { get => explodeSizeMulti; }
        public float ExplodeForceMulti { get => explodeForceMulti; }
        public int MinSplitNum { get => minSplitNum; }
        public int MaxSplitNum { get => maxSplitNum; }
    }
}