using System;
using UnityEngine;

/// <summary>
/// 第三人稱控制器
/// 移動.跳躍.基本控制.動畫更新
/// </summary>

namespace jerry
{
    public class vegas : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("移動速度"), Range(0, 50)]
        private float speed = 3.5f;
        [SerializeField, Header("旋轉速度"), Range(0, 50)]
        private float turn = 5f;
        [SerializeField, Header("跳躍速度"), Range(0, 50)]
        private float jump = 7f;

        private Animator ani;
        private CharacterController controller;
        #endregion

        private Vector3 direction;
        private Transform traCamera;
        private string parrun = "浮點數跑步";
        private string parjump = "跳";

        #region 事件
        private void Awake()
        {
            //getcomponent 抓取物件
            ani = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();

            traCamera = GameObject.Find("Main Camera").transform;
            // traCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
            //尋找物件.另外寫法
        }

        private void Update()
        {
            Move();
            Jump();
        }
        #endregion

        #region 方法
        // 移動
        private void Move()
        {
            float v = Input.GetAxisRaw("Vertical"); //取得按鍵值:WS .注意拼字大小寫正確
            float h = Input.GetAxisRaw("Horizontal"); //取得按鍵值:AD

            direction.x = h;
            direction.z = v;

            direction = transform.TransformDirection(direction);

            //time.deltatime 每幀時間.修正幀數速度差
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

            

            #region 攝影機旋轉
            //transform.rotation=traCamera.rotation;  //沒有過渡
            //玩家角度=四元數.插值(玩家角度.攝影機角度.速度X每幀時間))
            transform.rotation = Quaternion.Lerp(transform.rotation, traCamera.rotation, turn * Time.deltaTime);
            //eulerangles 角度0-45-90-180-360   XZ角度固定為0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            #endregion
        }

        //跳躍
        private void Jump()
        {
            if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jump;
                ani.SetTrigger(parjump);
            }
            //gravity 地吸引力 9.81 預設
            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        #endregion
    }
}

