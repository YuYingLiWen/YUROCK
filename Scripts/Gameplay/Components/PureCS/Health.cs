using System;

namespace YuSystem.Gameplay.Components
{
    public class Health
    {
        public readonly int MaxHP;

        public int HP { get; private set; }

        public Health(int hp)
        {
            this.HP = hp;
            this.MaxHP = hp;
        }

        public bool IsAlive => this.HP > 0;

        public void TakeDamage(int damage)
        {
            HP -= damage;
            OnTakeDamage?.Invoke(HP);

            if (!IsAlive) OnDeath?.Invoke();
        }

        public void Reset() => this.HP = MaxHP;

        public event Action OnDeath;

        public event Action<int> OnTakeDamage;
    }
}