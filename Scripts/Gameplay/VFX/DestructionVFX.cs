using UnityEngine;

namespace YuSystem.Gameplay.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DestructionVFX : Actor
    {
        protected ParticleSystem.MainModule main;

        protected void Awake()
        {
            main = GetComponent<ParticleSystem>().main;
        }

        protected override void Start()
        {
            base.Start();

            main.playOnAwake = true;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Disable();
        }
    }
}