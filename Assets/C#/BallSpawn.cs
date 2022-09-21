using UnityEngine;

namespace jerry
{
    public class BallSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefaBall;

        private void Awake()
        {
            InvokeRepeating("Spawn", 0, 0.1f);//���ƽե�
        }

        private void Spawn()//�]�w�ͦ��d��
        {
            Vector3 pos;
            pos.x = Random.Range(-15f, 15f);
            pos.y = Random.Range(5f, 7f);
            pos.z = Random.Range(-15f, 15f);

            Instantiate(prefaBall, pos, Quaternion.identity);//�����(X,X,�|���?!.����?!)
        }

    }
}
