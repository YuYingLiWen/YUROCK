using UnityEngine;
using YuSystem.Gameplay.Components;

namespace YuSystem.Gameplay
{
    public abstract class Spawner : MonoBehaviour, ISpawnable
    {
        protected abstract void IncreaseDifficulty();

        public abstract void Spawn();
    }
}