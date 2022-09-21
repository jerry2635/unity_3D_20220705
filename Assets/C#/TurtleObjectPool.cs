using UnityEngine.Pool;
using UnityEngine;
using jerry;

namespace jerry
{

    public class TurtleObjectPool : MonoBehaviour
    {
        [SerializeField, Header("包")]
        private GameObject prefaChips;
        [SerializeField, Header("包裝最大數量")]
        private int countMaxChips = 30;

        //物件池
        private ObjectPool<GameObject> poolChips;

        private int count;

        private void Awake()
        {
            poolChips = new ObjectPool<GameObject>(
                CreatePool, GetChips, ReleaseChips, DestroyChips, false, countMaxChips);
            //實體化物件(建立-取得-歸還-超限處置-是否輸出訊息-數量)
            InvokeRepeating("Spawn", 0, 0.1f);
        }

        private GameObject CreatePool()
        {
            count++;
            GameObject temp = Instantiate(prefaChips);
            temp.name = prefaChips.name + " " + count;
            return temp;
        }

        private void GetChips(GameObject Chips)
        {
            Chips.SetActive(true);
        }
        private void ReleaseChips(GameObject Chips)
        {
            Chips.SetActive(false);
        }
        private void DestroyChips(GameObject Chips)
        {
            Destroy(Chips);
        }

        public GameObject GetPoolObject()
        {
            return poolChips.Get();
        }

        public void ReleasePoolObject(GameObject Chips)
        {
            poolChips.Release(Chips);
        }
    }
}


