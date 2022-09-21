using UnityEngine;

namespace jerry
{
    public class BallObjectPool : MonoBehaviour
    {
        /// <summary>
        /// ¸I¼²¨Æ¥ó
        /// </summary>
        /// <param name="ball"></param>
        public delegate void delegateHit(GameObject ball);
        public delegateHit onHit;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Contains("Plane"))
            {
                onHit(gameObject);
            }
        }
    }
}

