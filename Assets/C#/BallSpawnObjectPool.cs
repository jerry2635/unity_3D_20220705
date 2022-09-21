using UnityEngine.Pool;
using UnityEngine;
using jerry;

public class BallSpawnObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefaBall;

    private void Awake()
    {
        poolBall = new ObjectPool<GameObject>(
            CreatePool, GetBall, ReleaseBall, DestroyBall, false, 100);
        //����ƪ���(�إ�-���o-�k��-�W���B�m-�O�_��X�T��-�ƶq)
        InvokeRepeating("Spawn", 0, 0.1f);
    }

    //�����
    private ObjectPool<GameObject> poolBall;

    private GameObject CreatePool()
    {
        return Instantiate(prefaBall);
    }

    private void GetBall(GameObject ball)//�򪫥�����o
    {
        ball.SetActive(true);
    }

    private void ReleaseBall(GameObject ball)//����/�k�ٵ������
    {
        ball.SetActive(true);
    }
    /// <summary>
    /// �W�X�ƶq���B�z/�}�a
    /// </summary>
    /// <param name="ball"></param>
    private void DestroyBall(GameObject ball)
    {
        Destroy(ball);
    }

    private void Spawn()//�]�w�ͦ��d��
    {
        Vector3 pos;
        pos.x = Random.Range(-15f, 15f);
        pos.y = Random.Range(5f, 7f);
        pos.z = Random.Range(-15f, 15f);
        
        //�򪫥�����o
        GameObject tempBall = poolBall.Get();
        tempBall.transform.position = pos;
        //����I���ƥ�(�l)-�����k��
        tempBall.GetComponent<BallObjectPool>().onHit = BallHitAndRelease;
    }

    private void BallHitAndRelease(GameObject ball)
    {
        poolBall.Release(ball);
    }
}
