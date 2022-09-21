using UnityEngine;

namespace jerry
{
    public class BallSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefaBall;

        private void Awake()
        {
            InvokeRepeating("Spawn", 0, 0.1f);//重複調用
        }

        private void Spawn()//設定生成範圍
        {
            Vector3 pos;
            pos.x = Random.Range(-15f, 15f);
            pos.y = Random.Range(5f, 7f);
            pos.z = Random.Range(-15f, 15f);

            Instantiate(prefaBall, pos, Quaternion.identity);//實體化(X,X,四位數?!.身分?!)
        }

    }
}
