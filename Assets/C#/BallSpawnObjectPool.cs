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
        //實體化物件(建立-取得-歸還-超限處置-是否輸出訊息-數量)
        InvokeRepeating("Spawn", 0, 0.1f);
    }

    //物件池
    private ObjectPool<GameObject> poolBall;

    private GameObject CreatePool()
    {
        return Instantiate(prefaBall);
    }

    private void GetBall(GameObject ball)//跟物件池取得
    {
        ball.SetActive(true);
    }

    private void ReleaseBall(GameObject ball)//釋放/歸還給物件池
    {
        ball.SetActive(true);
    }
    /// <summary>
    /// 超出數量的處理/破壞
    /// </summary>
    /// <param name="ball"></param>
    private void DestroyBall(GameObject ball)
    {
        Destroy(ball);
    }

    private void Spawn()//設定生成範圍
    {
        Vector3 pos;
        pos.x = Random.Range(-15f, 15f);
        pos.y = Random.Range(5f, 7f);
        pos.z = Random.Range(-15f, 15f);
        
        //跟物件池取得
        GameObject tempBall = poolBall.Get();
        tempBall.transform.position = pos;
        //抓取碰撞事件(子)-執行歸還
        tempBall.GetComponent<BallObjectPool>().onHit = BallHitAndRelease;
    }

    private void BallHitAndRelease(GameObject ball)
    {
        poolBall.Release(ball);
    }
}
