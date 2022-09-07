using UnityEngine;

namespace jerry
{

    public class PlayerHealth : HealthSystem
    {
        private vegas tpc;

        protected override void Awake()
        {
            base.Awake ();

            tpc = GetComponent<vegas>();
        }

        protected override void Dead()
        {
            base.Dead();
            tpc.enabled = false;
        }
    }
}

