using UnityEngine;

namespace jerry
{
    /// <summary>
    /// ���V��v��
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        private Transform traCamera;//�]�m�W��

        private void Awake()//�}���ɳ��:����D��v��
        {
            traCamera = Camera.main.transform;
        }
        private void Update() //�Y�ɧ�s
        {
            LookAt();
        }
        private void LookAt()//���VC# transform.LookAt
        {
            transform.LookAt(traCamera);
        }
    }
}

