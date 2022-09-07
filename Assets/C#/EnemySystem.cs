using UnityEngine;
using UnityEngine.AI;


namespace jerry
{
    /// <summary>
    /// �ĤH�t��:�l��.�C��.����
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�ĤH���")]
        private DataEnemy dataEnemy;
        [SerializeField]
        private StateEnemy stateEnemy;

        private Animator ani;
        private NavMeshAgent nma;
        private Vector3 v3TargetPosition;
        private string parWalk = "�����}��";
        private string parAttack = "Ĳ�o����";
        private float timerAttack;
        private float timerIdle;
        private EnemyAttack enemyAttack;

        #endregion

        #region �ƥ�
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
        /// �����ƥ�:�����O�Q�����ɰ���@��
        /// </summary>
        private void OnDisable()
        {
            //���i����:�Y�N�L��/�Q�R��  ��ĳ�ϥηs��API���N���
            nma.isStopped = true;
        }

        #region ��k
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
        /// �C��
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
        /// ����
        /// </summary>
        private void Idle()
        {
            //nma.velocity=Vector3.zero; //���z�Ʀ��k�s
            ani.SetBool(parWalk, false);
            timerIdle += Time.deltaTime;
            //print("���ݮɶ�:" + timeIdle);
            float r = Random.Range(dataEnemy.timeIdleRange.x, dataEnemy.timeIdleRange.y);
            if (timerIdle >= r)
            {
                timerIdle = 0;
                stateEnemy = StateEnemy.Wander;
            }
        }
        /// <summary>
        /// �l��
        /// </summary>
        private void Track()
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Ĳ�o����"))//�������ϼh�W��
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
        /// ����
        /// </summary>
        private void Attack()
        {
            ani.SetBool(parWalk, false);
            nma.velocity = Vector3.zero; //���ʺD�ʬ��s
            if (timerAttack < dataEnemy.intervalAttack)
            {
                timerAttack += Time.deltaTime; //����ɶ�
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

                if (stateEnemy == StateEnemy.Attack) return; //�p�G�ĤH �B�� ���O ���� )��^�W�q�{��

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

