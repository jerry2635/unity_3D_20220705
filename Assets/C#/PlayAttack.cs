using UnityEngine;

namespace jerry
{
    /// <summary>
    /// 玩家攻擊:輸入方式控制攻擊動畫與攻擊判定
    /// </summary>
    public class PlayAttack : AttackSystem
    {
        private Animator ani;
        private vegas tpc;

        private string parAttack = "攻擊";

        private void Awake()
        {
            ani = GetComponent<Animator>();
            tpc = GetComponent<vegas>();
        }

        private void Update()
        {
            AttackInput();
        }
        /// <summary>
        /// 輸入攻擊判定
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

