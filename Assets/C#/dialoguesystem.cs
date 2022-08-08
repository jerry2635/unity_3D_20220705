using TMPro;
using UnityEngine;
using System.Collections;
using System;

namespace jerry
{
    //�e��ñ�W.�L�Ǧ^.�L�Ѽ�
    public delegate void DelegateFinishDialogue();

    /// <summary>
    /// ��ܨt��.�H�J.��sNPC��ƦW��.���e.����.�H�X
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class dialoguesystem : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�e����ܨt��")]
        private CanvasGroup groupdialogue;
        [SerializeField, Header("���ܪ̦W��")]
        private TextMeshProUGUI textName;
        [SerializeField, Header("��ܤ��e")]
        private TextMeshProUGUI textContent;

        private AudioSource aud;
        #endregion

        [SerializeField, Header("�H�J���j")]
        private float intervalFadeIn = 0.1f;
        [SerializeField, Header("���r���j")]
        private float intervalType = 0.05f;

        [SerializeField, Header("��ܼХ�")]
        private GameObject gotriangle;

        private NPCdata dataNpc;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            //StartCoroutine(StartDialogue());
        }

        #region ���}��ƻP��k
        /// <summary>
        /// ��ܰ���P�_
        /// </summary>
        public bool isDialogue;

        #region ��P�о�
        ///��P�{�ǻݭn
        ///�ޥ�system.collection
        ///�w�q��k.�Ǧ^ieunumerator
        ///�Ұ�startcoroutine
        private IEnumerator test()
        {
            print("�Ĥ@���r");
            yield return new WaitForSeconds(2);
            print("�ĤG���r");
            yield return new WaitForSeconds(5);
            print("�ĤT���r");
        }
        #endregion

        public IEnumerator StartDialogue(NPCdata _dataNPC,DelegateFinishDialogue callback)
        {
            isDialogue = true;

            dataNpc = _dataNPC;

            textName.text = dataNpc.nameNPC;
            textContent.text = "";

            yield return StartCoroutine(Fade());

            for (int i=0;i< dataNpc.dataDialogue.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));

                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
            }
            StartCoroutine(Fade(false));
            isDialogue = false;
            callback(); //����I�s�{��
        }
        #endregion

        private IEnumerator Fade( bool fadeIn=true)
        {
            float increase = fadeIn ? 0.1f : -0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupdialogue.alpha += increase;
                yield return new WaitForSeconds(0.3f);
            }
        }

        private IEnumerator TypeEffect(int indexDialogue)
        {
            textContent.text = "";
            aud.PlayOneShot(dataNpc.dataDialogue[indexDialogue].sound);

            string content = dataNpc.dataDialogue[indexDialogue].content;

            for (int i = 0; i < content.Length; i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(0.05f);
            }

            gotriangle.SetActive(true);
        }
    }
}

