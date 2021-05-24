using YuSystem.Gameplay.Utilities;
using YuSystem.Managers;

namespace YuSystem.Gameplay
{
    public class PlayerSpawner : Spawner
    {
        private void OnEnable()
        {
            Yu_GameManager.Instance.OnGameStart += Spawn;
        }

        //private void OnDisable()
        //{
        //    MG_GameManager.Instance.OnGameStart -= Spawn;
        //}

        public override void Spawn()
        {
            GameObjectsPooler.Instance.Spawn(PoolerPrefabNames.Player, transform.position);
        }

        protected override void IncreaseDifficulty()
        {
            //Nothing
        }
    }
}
