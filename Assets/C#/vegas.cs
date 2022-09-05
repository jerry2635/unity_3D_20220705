using System;
using UnityEngine;

/// <summary>
/// �ĤT�H�ٱ��
/// ����.���D.�򥻱���.�ʵe��s
/// </summary>

namespace jerry
{
    public class vegas : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("���ʳt��"), Range(0, 50)]
        private float speed = 3.5f;
        [SerializeField, Header("����t��"), Range(0, 50)]
        private float turn = 5f;
        [SerializeField, Header("���D�t��"), Range(0, 50)]
        private float jump = 7f;

        private Animator ani;
        private CharacterController controller;
        #endregion

        private Vector3 direction;
        private Transform traCamera;
        private string parrun = "�B�I�ƶ]�B";
        private string parjump = "��";

        #region �ƥ�
        private void Awake()
        {
            //getcomponent �������
            ani = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();

            traCamera = GameObject.Find("Main Camera").transform;
            // traCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
            //�M�䪫��.�t�~�g�k
        }

        private void Update()
        {
            Move();
            Jump();
        }
        #endregion

        #region ��k
        // ����
        private void Move()
        {
            float v = Input.GetAxisRaw("Vertical"); //���o�����:WS .�`�N���r�j�p�g���T
            float h = Input.GetAxisRaw("Horizontal"); //���o�����:AD

            direction.x = h;
            direction.z = v;

            direction = transform.TransformDirection(direction);

            //time.deltatime �C�V�ɶ�.�ץ��V�Ƴt�׮t
            controller.Move(direction * speed*Time.deltaTime);

            float vAxis = Input.GetAxis("Vertical");
            float hAxis = Input.GetAxis("Horizontal");

            if (Math.Abs(vAxis) > 0.1f)
            {
                ani.SetFloat(parrun, Mathf.Abs(vAxis));
            }
            else if(Math.Abs(hAxis)>0.1f)
            {
                ani.SetFloat(parrun, Mathf.Abs(hAxis));
            }
            else
            {
                ani.SetFloat(parrun, 0);
            }

            

            #region ��v������
            //transform.rotation=traCamera.rotation;  //�S���L��
            //���a����=�|����.����(���a����.��v������.�t��X�C�V�ɶ�))
            transform.rotation = Quaternion.Lerp(transform.rotation, traCamera.rotation, turn * Time.deltaTime);
            //eulerangles ����0-45-90-180-360   XZ���שT�w��0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            #endregion
        }

        //���D
        private void Jump()
        {
            if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jump;
                ani.SetTrigger(parjump);
            }
            //gravity �a�l�ޤO 9.81 �w�]
            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        #endregion
    }
}

