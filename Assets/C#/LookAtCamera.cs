using UnityEngine;

namespace jerry
{
    /// <summary>
    /// 面向攝影機
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        private Transform traCamera;//設置名稱

        private void Awake()//開機時喚醒:抓取主攝影機
        {
            traCamera = Camera.main.transform;
        }
        private void Update() //即時更新
        {
            LookAt();
        }
        private void LookAt()//面向C# transform.LookAt
        {
            transform.LookAt(traCamera);
        }
    }
}

