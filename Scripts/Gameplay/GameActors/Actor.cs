using UnityEngine;
using YuSystem.Gameplay.Components;

namespace YuSystem.Gameplay
{
    public abstract class Actor : MonoBehaviour, IDisable
    {

        protected virtual void Start()
        {
            //Forces the obj to not be on the Z axis
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
