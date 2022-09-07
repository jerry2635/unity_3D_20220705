using UnityEngine;
using System.Collections;

namespace jerry
{
    public class AttackSystem : MonoBehaviour //MonoBehaviour可放置在物件上的類型
    {
        [SerializeField, Header("攻擊資料")]
        private DataAttack dataAttack;
        [SerializeField, Header("攻擊動畫名稱")]
        private string nameAnimation;

        protected bool canAttack = true;
        protected Animator ani;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = dataAttack.attackAreaColor;

            Gizmos.matrix = Matrix4x4.TRS(
                transform.position + transform.TransformDirection(dataAttack.attackAreaOffset),
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(
                Vector3.zero,
                dataAttack.attackAreaSize);
        }

        public void StartAttack()
        {
            if (!canAttack) return;
            StartCoroutine(AttackFlow());
        }

        private IEnumerator AttackFlow()
        {
            canAttack = false;
            yield return new WaitForSeconds(dataAttack.delayAttack);
            CheckAttackArea();

            yield return new WaitForSeconds(dataAttack.waitAttackEnd);
            canAttack = true;
            StopAttack();
        }

        protected virtual void StopAttack()
        {

        }

        public void CheckAttackArea()
        {
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName(nameAnimation)) return;

            Collider[] hits = Physics.OverlapBox(
              transform.position +
              transform.TransformDirection(dataAttack.attackAreaOffset),
              dataAttack.attackAreaSize / 2,
              transform.rotation, dataAttack.layerTarget);

            if (hits.Length > 0)
            {
                hits[0].GetComponent<HealthSystem>().Hurt(dataAttack.attack);//呼叫血量系統.受傷(接收攻擊資料)
            }
        }
    }



}

