using UnityEngine;
using UnityEngine.UI;

namespace jerry
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("血量資料")]//產生放置data欄位
        protected DataHealth dataHealth;
        [SerializeField, Header("血條")]
        private Image imgHealth;

        private float hp;
        private Animator ani;
        private string parHurt = "受傷";
        private string parDead = "死亡";
        private AttackSystem attackSystem;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;
        }
        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(float damage)
        {
            hp -= damage;
            ani.SetTrigger(parHurt);

            if (hp <= 0) Dead();//當hp<=0啟動Dead

            imgHealth.fillAmount = hp / dataHealth.hpMax;//圖像血條= hp(變動項)/ 血量資料/最大值
        }
        /// <summary>
        /// 死亡
        /// </summary>
        protected virtual void Dead()//設定成保護型虛擬資料.避免被複寫
        {
            hp = 0;
            ani.SetBool(parDead, true);//啟動bool死亡
            attackSystem.enabled = false;
        }
    }
}

