using UnityEngine;
using System.Collections;

namespace jerry
{

    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;
        private Material matdissolve; //命名一個材質
        private string nameDissolve = "DissolveValue";//材質名稱=系統內名稱
        private float maxDissolve = 2.5f;
        private float minDissolve = -2.6f;

        private TurtleObjectPool objectPoolChips;

        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();
            matdissolve = GetComponentsInChildren<Renderer>()[0].material;//抓取怪物皮膚網格渲染器.子物件
            //matdissolve.SetFloat(nameDissolve, 0.1f);//設定浮點數.測試
            objectPoolChips = FindObjectOfType<TurtleObjectPool>();
        }

        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;
            DropProp();
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()//計算式?!
        {
            while (maxDissolve > minDissolve)
            {
                maxDissolve = 0.1f;
                matdissolve.SetFloat(nameDissolve, maxDissolve);
                yield return new WaitForSeconds(0.03f);
            }
        }

        /// <summary>
        /// 掉落道具
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

