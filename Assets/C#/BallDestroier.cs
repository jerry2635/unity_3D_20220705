using UnityEngine;

namespace jerry
{
    public class BallDestroier : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)//collision �I��
        {
            if (collision.gameObject.name.Contains("Plane"))
            {
                Destroy(gameObject);
            }
        }
    }
}

