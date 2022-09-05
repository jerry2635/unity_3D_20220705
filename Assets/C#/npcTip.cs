using UnityEngine;

namespace jerry
{
    /// <summary>
    /// �ϰ�i�J����.��ܵe��.���䰻���αҰ�
    /// </summary>
    public class npcTip : MonoBehaviour
    {
        [SerializeField, Header("npc ��ܸ��")]
        private NPCdata dataNPC;
        [SerializeField, Header("NPC��v��")]
        private GameObject goCamera;

        /// <summary>
        /// ���ܩ���
        /// </summary>
        private Animator aniTi;
        private string parTipFade = "Ĳ�o�H�X�H�J";
        private bool isInTrigger;
        private vegas Vegas;
        private dialoguesystem dialoguesystem;
        private Animator ani;
        private string parDialogue = "��ܶ}��";

        private void Awake()
        {
            aniTi = GameObject.Find("�Ϭۮ�").GetComponent<Animator>();
            Vegas = FindObjectOfType<vegas>();
            dialoguesystem = FindObjectOfType<dialoguesystem>();
            ani = GetComponent<Animator>();
        }
        
        //�I���ƥ�
        //��@����㦳rigidbody
        //�Ŀ�trigger.�ϥ�ontrigger�ƥ�.enter exit stay

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
        /// �����i�J�����}.��s�ʵe
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

