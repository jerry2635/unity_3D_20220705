using UnityEngine;
using UnityEngine.AI;


namespace jerry
{
    /// <summary>
    /// 敵人系統:追蹤.遊走.攻擊
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("敵人資料")]
        private DataEnemy dataEnemy;
        [SerializeField]
        private StateEnemy stateEnemy;

        private Animator ani;
        private NavMeshAgent nma;
        private Vector3 v3TargetPosition;
        private string parWalk = "走路開關";
        private string parAttack = "觸發攻擊";
        private float timerAttack;
        private float timerIdle;
        private EnemyAttack enemyAttack;

        #endregion

        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>();
            enemyAttack = GetComponent<EnemyAttack>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = dataEnemy.speedWalk;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeTrack);

            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeAttack);

            Gizmos.color = new Color(1, 0.2f, 0.3f, 1);
            Gizmos.DrawSphere(v3TargetPosition, 0.3f);
        }

        private void Update()
        {
            StateSwitcher();
            CheckTargetInTrackRange();
        }
        #endregion
        /// <summary>
        /// 關閉事件:此類別被關閉時執行一次
        /// </summary>
        private void OnDisable()
        {
            //綠色波浪符:即將過時/被刪除  建議使用新的API替代方案
            nma.isStopped = true;
        }

        #region 方法
        private void StateSwitcher()
        {
            switch (stateEnemy)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Wander:
                    Wander();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
            }
        }
        /// <summary>
        /// 遊走
        /// </summary>
        private void Wander()
        {
            if (nma.remainingDistance == 0)
            {
                v3TargetPosition =transform.position+ Random.insideUnitSphere * dataEnemy.rangeTrack;
                v3TargetPosition.y = transform.position.y;
            }

            nma.SetDestination(v3TargetPosition);
            ani.SetBool(parWalk, nma.velocity.magnitude > 0.2f);
        }
        /// <summary>
        /// 等待
        /// </summary>
        private void Idle()
        {
            //nma.velocity=Vector3.zero; //物理滑行歸零
            ani.SetBool(parWalk, false);
            timerIdle += Time.deltaTime;
            //print("等待時間:" + timeIdle);
            float r = Random.Range(dataEnemy.timeIdleRange.x, dataEnemy.timeIdleRange.y);
            if (timerIdle >= r)
            {
                timerIdle = 0;
                stateEnemy = StateEnemy.Wander;
            }
        }
        /// <summary>
        /// 追蹤
        /// </summary>
        private void Track()
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("觸發攻擊"))//抓取控制器圖層名稱
            {
                nma.velocity = Vector3.zero;
            }

            nma.SetDestination(v3TargetPosition);
            ani.SetBool(parWalk,true);
            ani.ResetTrigger(parAttack);

            if (Vector3.Distance(transform.position,v3TargetPosition) <= dataEnemy.rangeAttack)
            {
                stateEnemy = StateEnemy.Attack;
            }
            else
            {
                timerAttack = dataEnemy.intervalAttack;
            }
        }

        /// <summary>
        /// 攻擊
        /// </summary>
        private void Attack()
        {
            ani.SetBool(parWalk, false);
            nma.velocity = Vector3.zero; //移動慣性為零
            if (timerAttack < dataEnemy.intervalAttack)
            {
                timerAttack += Time.deltaTime; //延遲時間
            }
            else
            {
                ani.SetTrigger(parAttack);
                timerAttack = 0;
                enemyAttack.StartAttack();
                stateEnemy = StateEnemy.Track;
            }
        }

        private void CheckTargetInTrackRange()
        {
           

            Collider[] hits = Physics.OverlapSphere(transform.position, dataEnemy.rangeTrack, dataEnemy.layerTarget);
            if (hits.Length > 0)
            {
                v3TargetPosition = hits[0].transform.position;

                if (stateEnemy == StateEnemy.Attack) return; //如果敵人 處於 類別 攻擊 )返回上段程式

                stateEnemy = StateEnemy.Track;
            }
            else
            {
                stateEnemy = StateEnemy.Wander;
            }
        }
        #endregion
    }

}

