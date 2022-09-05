using UnityEngine;

namespace jerry
{
    /// <summary>
    /// 區域進入偵測.顯示畫面.按鍵偵測及啟動
    /// </summary>
    public class npcTip : MonoBehaviour
    {
        [SerializeField, Header("npc 對話資料")]
        private NPCdata dataNPC;
        [SerializeField, Header("NPC攝影機")]
        private GameObject goCamera;

        /// <summary>
        /// 提示底圖
        /// </summary>
        private Animator aniTi;
        private string parTipFade = "觸發淡出淡入";
        private bool isInTrigger;
        private vegas Vegas;
        private dialoguesystem dialoguesystem;
        private Animator ani;
        private string parDialogue = "對話開關";

        private void Awake()
        {
            aniTi = GameObject.Find("圖相框").GetComponent<Animator>();
            Vegas = FindObjectOfType<vegas>();
            dialoguesystem = FindObjectOfType<dialoguesystem>();
            ani = GetComponent<Animator>();
        }
        
        //碰撞事件
        //其一物件具有rigidbody
        //勾選trigger.使用ontrigger事件.enter exit stay

        private void OnTriggerEnter(Collider other)
        {
            CheckPlayerAndAnimation(other.name,true);
        }

        private void OnTriggerExit(Collider other)
        {
            CheckPlayerAndAnimation(other.name,false);
        }

        private void Update()
        {
            InputKeyAndStartDialogue();
        }
        /// <summary>
        /// 偵測進入或離開.更新動畫
        /// </summary>
        private void CheckPlayerAndAnimation(string nameHit,bool _isInTrigger)
        {
           if(nameHit=="bigvegas")
            {
                isInTrigger = _isInTrigger;
                aniTi.SetTrigger(parTipFade);
            }
        }

        private void InputKeyAndStartDialogue()
        {
            if (dialoguesystem.isDialogue) return;

            if (isInTrigger && Input.GetKeyDown(KeyCode.E))
            {
                goCamera.SetActive(true);
                aniTi.SetTrigger(parTipFade);
                Vegas.enabled = false;
                try
                {
                    ani.SetBool(parDialogue, true);
                }
                catch (System.Exception)
                {
                    throw;
                }
                StartCoroutine(dialoguesystem.StartDialogue(dataNPC,ResetControllerAndCloseCamera));
            }
        }

        private void ResetControllerAndCloseCamera()
        {
            goCamera.SetActive(false);
            Vegas.enabled = true;
            aniTi.SetTrigger(parTipFade);
            try
            {
                ani.SetBool(parDialogue,false);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

