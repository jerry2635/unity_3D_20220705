using UnityEngine;

namespace jerry
{
    /// <summary>
    /// ���a����:��J�覡��������ʵe�P�����P�w
    /// </summary>
    public class PlayAttack : AttackSystem
    {
        private vegas tpc;

        private string parAttack = "Ĳ�o����";

        protected override void Awake()
        {
            base.Awake();
            tpc = GetComponent<vegas>();
        }

        private void Update()
        {
            AttackInput();
        }
        /// <summary>
        /// ��J�����P�w
        /// </summary>
        private void AttackInput()
        {
            if (!canAttack) return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tpc.enabled = false;
                ani.SetTrigger(parAttack);
                StartAttack();
            }
        }

        protected override void StopAttack()
        {
            tpc.enabled = true;
        }
    }

}

