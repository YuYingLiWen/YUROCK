
using UnityEngine;

namespace YuSystem.Gameplay.Utilities
{
    public class TagAndLayer : MonoBehaviour
    {
        [SerializeField] private Tags objTag = Tags.Untagged;
        [SerializeField] private Layers layer = Layers.Defaut;

        private void Awake()
        {
            gameObject.tag = $"{objTag}";
            gameObject.layer = LayerMask.NameToLayer($"{layer}");
        }
    }
}
