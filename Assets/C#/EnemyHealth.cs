using UnityEngine;
using System.Collections;

namespace jerry
{

    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;
        private Material matdissolve; //�R�W�@�ӧ���
        private string nameDissolve = "DissolveValue";//����W��=�t�Τ��W��
        private float maxDissolve = 2.5f;
        private float minDissolve = -2.6f;

        private TurtleObjectPool objectPoolChips;

        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();
            matdissolve = GetComponentsInChildren<Renderer>()[0].material;//����Ǫ��ֽ������V��.�l����
            //matdissolve.SetFloat(nameDissolve, 0.1f);//�]�w�B�I��.����
            objectPoolChips = FindObjectOfType<TurtleObjectPool>();
        }

        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;
            DropProp();
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()//�p�⦡?!
        {
            while (maxDissolve > minDissolve)
            {
                maxDissolve = 0.1f;
                matdissolve.SetFloat(nameDissolve, maxDissolve);
                yield return new WaitForSeconds(0.03f);
            }
        }

        /// <summary>
        /// �����D��
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;

            if (value <= dataHealth.propProbability)
            {
                // Instantiate(
                // dataHealth.goProp,
                // transform.position + Vector3.up,
                // Quaternion.identity);

                GameObject tempObject = objectPoolChips.GetPoolObject();
                tempObject.transform.position = transform.position + Vector3.up * 3;
            }
        }
    }
}

