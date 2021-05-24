using UnityEngine;
using YuSystem.Gameplay.ScriptableObjects;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class ExtraPoints : Collectibles
    {
        [SerializeField] protected SO_PowerUp script;

        protected MeshFilter meshF;
        protected int rotDirection = 1;

        protected override void Awake()
        {
            base.Awake();

            meshF = GetComponent<MeshFilter>();
        }

        protected virtual void OnEnable()
        {
            rotDirection = script.SpinRotation();
        }

        protected override void Update()
        {
            rigid.MovePosition(rigid.position - Vector3.up * script.Speed * Time.deltaTime);
            rigid.MoveRotation(rigid.rotation * Quaternion.Euler(Vector3.up * rotDirection * script.RotSpeed * Time.deltaTime));
        }

        protected override void AddScore()
        {
            Yu_StatsManager.Instance.AddScorePowerUps(script.Score);
        }
    }
}


