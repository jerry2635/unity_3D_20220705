using UnityEngine.Pool;
using UnityEngine;
using jerry;

namespace jerry
{

    public class TurtleObjectPool : MonoBehaviour
    {
        [SerializeField, Header("�]")]
        private GameObject prefaChips;
        [SerializeField, Header("�]�˳̤j�ƶq")]
        private int countMaxChips = 30;

        //�����
        private ObjectPool<GameObject> poolChips;

        private int count;

        private void Awake()
        {
            poolChips = new ObjectPool<GameObject>(
                CreatePool, GetChips, ReleaseChips, DestroyChips, false, countMaxChips);
            //����ƪ���(�إ�-���o-�k��-�W���B�m-�O�_��X�T��-�ƶq)
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


