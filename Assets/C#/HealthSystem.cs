using UnityEngine;
using UnityEngine.UI;

namespace jerry
{
    /// <summary>
    /// ��q�t��
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("��q���")]//���ͩ�mdata���
        protected DataHealth dataHealth;
        [SerializeField, Header("���")]
        private Image imgHealth;

        private float hp;
        private Animator ani;
        private string parHurt = "����";
        private string parDead = "���`";
        private AttackSystem attackSystem;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(float damage)
        {
            hp -= damage;
            ani.SetTrigger(parHurt);

            if (hp <= 0) Dead();//��hp<=0�Ұ�Dead

            imgHealth.fillAmount = hp / dataHealth.hpMax;//�Ϲ����= hp(�ܰʶ�)/ ��q���/�̤j��
        }
        /// <summary>
        /// ���`
        /// </summary>
        protected virtual void Dead()//�]�w���O�@���������.�קK�Q�Ƽg
        {
            hp = 0;
            ani.SetBool(parDead, true);//�Ұ�bool���`
            attackSystem.enabled = false;
        }
    }
}

